using DataAccess.Interfaces;
using DataAccess.Storing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.APIAccess
{
    class MockFactory : IMdToHtmlAndContentsFactory
    {
        public Task<string> GetHtml(string markDown)
        {
            return new Task<string>(()=>"This is html");
        }

        public async Task<IHtmlAndContents> GetHtmlAndContents(string markDown)
        {
            return new HtmlAndContents()
            {
                Contents = new List<Contents>(),
                PageHTML = await GetHtml(markDown)
            };
        }
    }
}
