using DataAccess.Interfaces;
using DataAccess.Models;
using System;

namespace DataAccess.Repositories
{
    //SMELL#38: Same as #35 (inheritance of IPageRepository unnecessary)
    public class PageRepositoryStoring : PageRepositoryRetrieving
    {
        public PageRepositoryStoring(ViewzDbContext db, IMdToHtmlAndContentsFactory factory) : base(db, factory)
        {
        }

        protected override async System.Threading.Tasks.Task SetMDAsync(long pageID, string content)
        {
            await base.SetMDAsync(pageID, content);
            try
            {
                IHtmlAndContents result = await _factory.GetHtmlAndContents(content);
                await base.SetHTMLAsync(pageID, result?.PageHTML);
                await base.SetContentsAsync(pageID, result?.Contents);
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
