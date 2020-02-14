using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class PageRepositoryRetrieving : PageRepository, IPageRepository
    {
        protected readonly IMdToHtmlAndContentsFactory _factory;

        public PageRepositoryRetrieving(ViewzDbContext db, IMdToHtmlAndContentsFactory factory) : base(db)
        {
            _factory = factory;
        }
        protected override string GetHTML(long pageID)
        {
            try
            {
                return base.GetHTML(pageID);
            }
            catch (InvalidOperationException)
            {
                IHtmlAndContents result = _factory.GetHtmlAndContents(base.GetMD(pageID));
                base.SetHTML(pageID, result.PageHTML);
                base.SetContents(pageID, result.Contents);
                return result.PageHTML;
            }
        }
    }
}
