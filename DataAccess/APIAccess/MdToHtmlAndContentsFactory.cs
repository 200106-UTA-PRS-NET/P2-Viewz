using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using DataAccess.Storing;
using HtmlAgilityPack;

namespace DataAccess.APIAccess
{
    public class MdToHtmlAndContentsFactory : IMdToHtmlAndContentsFactory
    {
        protected HttpClient client;
        private const string url = "https://api.github.com/markdown/raw";

        public MdToHtmlAndContentsFactory()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "P2-Viewz");
        }

        public IHtmlAndContents GetHtmlAndContents(string markDown)
        {
            if (markDown == null)
                return null;

            HtmlAndContents RESULT = new HtmlAndContents
            {
                PageHTML = GetHtmlAsync(markDown).Result
            };
            RESULT.Contents = AParser(RESULT.PageHTML);
            return RESULT;
        }

        public string GetHtml(string markDown)
        {
            return GetHtmlAsync(markDown).Result;
        }
        async Task<string> GetHtmlAsync(string markDown)
        {
            if (markDown == null)
                return null;

            var content = new StringContent(markDown, Encoding.UTF8, "text/plain");
            
            HttpResponseMessage _response = await client.PostAsync(url, content);
            if((int)_response.StatusCode != 200)
                throw new HttpRequestException();

            return await _response.Content.ReadAsStringAsync();
        }

        private static IEnumerable<Contents> AParser(string pagehtml)
        {
            var list = new List<Contents>();
            var HtmlDoc = new HtmlDocument();

            HtmlDoc.LoadHtml(pagehtml);
            var xpath = "//*[self::h1 or self::h2 or self::h3]";
            HtmlNode[] headers = HtmlDoc.DocumentNode.SelectNodes(xpath).ToArray();
            int id_count = 0;
            foreach(var h in headers)
            {
                HtmlNode id = h.SelectSingleNode(".//a");
                if (id != null)
                    id_count++;

                var C = new Contents
                {
                    Id = id?.Id,
                    Content = h.InnerText.Trim('\n')
                };
                switch (h.Name.ToLower())
                {
                    case "h1":
                        C.Level = 1;
                        break;
                    case "h2":
                        C.Level = 2;
                        break;
                    case "h3":
                        C.Level = 3;
                        break;
                }
                
                list.Add(C);
            }

            return list;
        }
    }
}
