using DataAccess.Interfaces;
using DataAccess.Models;
using System;

namespace DataAccess.Repositories
{
    //SMELL#35: IPageRepository removed from inheritance list (PageRepository implements Interface)
    public class PageRepositoryRetrieving : PageRepository
    {
        protected readonly IMdToHtmlAndContentsFactory _factory;

        public PageRepositoryRetrieving(ViewzDbContext db, IMdToHtmlAndContentsFactory factory) : base(db)
        {
            _factory = factory;
        }
        protected override async System.Threading.Tasks.Task<string> GetHTMLAsync(long pageID)
        {
            try
            {
                return await base.GetHTMLAsync(pageID);
            }
            catch (InvalidOperationException)
            {
                IHtmlAndContents result = await _factory.GetHtmlAndContents(await base.GetMDAsync(pageID));
                await base.SetHTMLAsync(pageID, result?.PageHTML);
                await base.SetContentsAsync(pageID, result?.Contents);
                return result?.PageHTML;
            }
        }
    }
}
