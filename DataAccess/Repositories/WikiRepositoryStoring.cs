using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class WikiRepositoryStoring: WikiRepositoryRetrieving, IWikiRepository
    {
        public WikiRepositoryStoring(ViewzDbContext db, IMdToHtmlAndContentsFactory factory) : base(db, factory)
        {
        }
        protected async override Task SetMDAsync(int wikiId, string content)
        {
            await base.SetMDAsync(wikiId, content);
            try
            {
                string result = await _factory.GetHtml(content);
                await base.SetHTMLAsync(wikiId, result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
