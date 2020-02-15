using DataAccess.Interfaces;
using DataAccess.Models;
using System;
//using System.Collections.Generic;
//using System.Text;

namespace DataAccess.Repositories
{
    //SMELL#43: Inheritance of interface unnecessary
    class WikiRepositoryRetrieving : WikiRepository
    {
        protected readonly IMdToHtmlAndContentsFactory _factory;

        public WikiRepositoryRetrieving(ViewzDbContext db, IMdToHtmlAndContentsFactory factory) : base(db)
        {
            _factory = factory;
        }
        protected override string GetHTML(int wikiId)
        {
            try {
                return base.GetHTML(wikiId);
            } catch (InvalidOperationException)
            {
                string result = _factory.GetHtml(base.GetMD(wikiId));
                base.SetHTML(wikiId, result);
                return result;
            }
        }
    }
}
