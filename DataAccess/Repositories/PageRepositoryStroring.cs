using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    internal class PageRepositoryStroring : PageRepositoryRetrieving
    {
        internal PageRepositoryStroring(ViewzDbContext db, IApiConnectionFactory factory) : base(db, factory)
        {
        }

        protected override void SetMD(long pageID, string content)
        {
            base.SetMD(pageID, content);
            try
            {
                IApiResult result = _factory.GetResult(content);
                base.SetHTML(pageID, result.PageHTML);
                base.SetDetails(pageID, result.Contents);
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
