using DataAccess.Interfaces;
using DataAccess.Models;
using System;
//using System.Collections.Generic;
//using System.Text;

namespace DataAccess.Repositories
{
    //SMELL#38: Same as #35 (inheritance of IPageRepository unnecessary)
    public class PageRepositoryStoring : PageRepositoryRetrieving
    {
        public PageRepositoryStoring(ViewzDbContext db, IMdToHtmlAndContentsFactory factory) : base(db, factory)
        {
        }

        protected override void SetMD(long pageID, string content)
        {
            base.SetMD(pageID, content);
            try
            {
                IHtmlAndContents result = _factory.GetHtmlAndContents(content);
                base.SetHTML(pageID, result?.PageHTML);
                base.SetContents(pageID, result?.Contents);
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
