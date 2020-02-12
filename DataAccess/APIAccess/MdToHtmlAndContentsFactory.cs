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
        private HttpClient client;
        public MdToHtmlAndContentsFactory()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "P2-Viewz");
        }

        public IHtmlAndContents GetResult(string markDown)
        {
            return GetResultAsync(markDown).Result;
        }
        private async Task<IHtmlAndContents> GetResultAsync(string markDown)
        {
            
            var content = new StringContent(markDown, Encoding.UTF8, "text/plain");
            if (content == null)
                return null;

            HttpResponseMessage _response = await client.PostAsync("https://api.github.com/markdown/raw", content);
            if((int)_response.StatusCode != 200)
                throw new HttpRequestException();
                
            var RESULT = new HtmlAndContents();
            RESULT.PageHTML = await _response.Content.ReadAsStringAsync();
            RESULT.Contents = AParser(RESULT.PageHTML);
            return RESULT;
        }

        private static IEnumerable<Contents> AParser(string pagehtml)
        {
            var list = new List<Contents>();
            var HtmlDoc = new HtmlDocument();

            HtmlDoc.LoadHtml(pagehtml);
            var xpath = "//*[self::h1 or self::h2 or self::h3]";
            HtmlNode[] headers = HtmlDoc.DocumentNode.SelectNodes(xpath).ToArray();
            foreach(var h in headers)
            {
                HtmlNode id = h.SelectSingleNode(".//a");

                var C = new Contents();
                C.Id = id.Id;
                C.Content = h.InnerText.Trim('\n');
                switch(h.Name.ToLower())
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
                    //default:                                      //TODO delete after testing
                        //throw new NotImplementedException();
                }
     
                list.Add(C);
            }

            return list;
        }
    }
}
