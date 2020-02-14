using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    class WikiRepositoryStoring: WikiRepositoryRetrieving, IWikiRepository
    {
        public WikiRepositoryStoring(ViewzDbContext db, IMdToHtmlAndContentsFactory factory) : base(db, factory)
        {
        }
        protected override void SetMD(int wikiId, string content)
        {
            base.SetMD(wikiId, content);
            try
            {
                string result = _factory.GetHtml(content);
                base.SetHTML(wikiId, result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
