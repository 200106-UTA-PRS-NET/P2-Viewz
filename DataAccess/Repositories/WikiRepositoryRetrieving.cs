using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class WikiRepositoryRetrieving : WikiRepository, IWikiRepository
    {
        protected readonly IMdToHtmlAndContentsFactory _factory;

        public WikiRepositoryRetrieving(ViewzDbContext db, IMdToHtmlAndContentsFactory factory) : base(db)
        {
            _factory = factory;
        }
        protected async override Task<string> GetHTMLAsync(int wikiId)
        {
            try {
                return await base.GetHTMLAsync(wikiId);
            } catch (InvalidOperationException)
            {
                string result = await _factory.GetHtml(await base.GetMDAsync(wikiId));
                await base.SetHTMLAsync(wikiId, result);
                return result;
            }
        }
    }
}
