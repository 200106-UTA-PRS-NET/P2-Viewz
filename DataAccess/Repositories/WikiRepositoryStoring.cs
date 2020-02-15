using DataAccess.Interfaces;
using DataAccess.Models;
using System;
//using System.Collections.Generic;
//using System.Text;

namespace DataAccess.Repositories
{
    //SMELL#46: Inheritance of interface unnecessary
    class WikiRepositoryStoring: WikiRepositoryRetrieving
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
