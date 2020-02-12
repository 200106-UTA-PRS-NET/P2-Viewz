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
                HttpResponseMessage _response = await client.PostAsync("https://api.github.com/markdown/raw", content);
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
                var C = new Contents()
                {
                    Id = id.Id,
                    Content = h.InnerText.Trim('\n')
                };
                list.Add(C);
            }

            return list;
        }
    }
}
