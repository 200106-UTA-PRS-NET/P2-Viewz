using DataAccess.Interfaces;
using DataAccess.Models;
using System;

namespace DataAccess.Repositories
{
    public class WikiRepositoryRetrieving : WikiRepository, IWikiRepository
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
