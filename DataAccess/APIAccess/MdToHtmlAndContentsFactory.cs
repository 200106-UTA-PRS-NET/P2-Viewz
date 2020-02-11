using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.APIAccess
{
    public class MdToHtmlAndContentsFactory
    {
        public IHtmlAndContents GetResult(string markDown)
        {
            return GetResultAsync(markDown).Result;
        }
        private async Task<IHtmlAndContents> GetResultAsync(string markDown)
        {
            HttpClient client = new HttpClient();
            try
            {
                var content = new StringContent(markDown, Encoding.UTF8, "text/plain");
                HttpResponseMessage response = await client.PostAsync("https://api.github.com/markdown/raw", content);
                return new HtmlAndContents()
                {
                    PageHTML = await response.Content.ReadAsStringAsync()
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
