using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Storing;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class WikiRepository : IWikiRepository
    {
        readonly ViewzDbContext _db;
        public WikiRepository(ViewzDbContext db)
        {
            _db = db;
        }
        int GetId(string wikiURL)
        {
            return (from wiki in _db.Wiki
                    where wiki.Url == wikiURL
                    select wiki.Id).Single();
        }
        public string GetHTML(string wikiURL)
        {
            return GetHTML(GetId(wikiURL));
        }

        private string GetHTML(int wikiId)
        {
            return (from content in _db.WikiHtmlDescription
                    where content.WikiId == wikiId
                    select content.HtmlDescription).First();
        }

        public string GetMD(string wikiURL)
        {
            return GetMD(GetId(wikiURL));
        }

        private string GetMD(int wikiId)
        {
            return (from content in _db.WikiMdDescription
                    where content.WikiId == wikiId
                    select content.MdDescription).Single();
        }

        public IEnumerable<Storing.Wiki> GetPopularWikis(uint count, bool description = false)
        {
            IEnumerable<Models.Wiki> wikis = (from wiki in _db.Wiki
                         orderby wiki.HitCount descending
                         select wiki)
                        .Take((int)count).ToList();
            return wikis.Select(w => {
                Storing.Wiki wiki = Mapper.Map(w);
                wiki.HtmlDescription = (description) ? GetHTML(w.Id) : null;
                return wiki; });

        }

        public Storing.Wiki GetWiki(string wikiURL)
        {
            return GetWiki(GetId(wikiURL));
        }

        private Storing.Wiki GetWiki(int wikiId)
        {
            var wiki = (from inWiki in _db.Wiki
                               where inWiki.Id == wikiId
                               select inWiki).Single();
            wiki.HitCount += 1;
            _db.SaveChanges();
            return Mapper.Map(wiki);
        }

        public Storing.Wiki GetWikiWithHTML(string wikiURL)
        {
            return GetWikiWithHTML(GetId(wikiURL));
        }

        private Storing.Wiki GetWikiWithHTML(int wikiId)
        {
            var modelsWiki = (from inwiki in _db.Wiki
                              where inwiki.Id == wikiId
                              select inwiki)
                              .Include(o => o.WikiHtmlDescription)
                              .Single();
            modelsWiki.HitCount += 1;
            var dbSave = _db.SaveChangesAsync();
            Storing.Wiki wiki = Mapper.Map(modelsWiki);
            if (wiki.HtmlDescription == null)
            {
                wiki.HtmlDescription = GetHTML(wikiId);
            }
            dbSave.Wait();
            return wiki;
        }

        public Storing.Wiki GetWikiWithMD(string wikiURL)
        {
            return GetWikiWithMD(GetId(wikiURL));
        }

        private Storing.Wiki GetWikiWithMD(int wikiId)
        {
            var wiki = (from inWiki in _db.Wiki
                        where inWiki.Id == wikiId
                        select inWiki)
                        .Include(o => o.WikiMdDescription)
                        .Single();
            wiki.HitCount += 1;
            _db.SaveChanges();
            return Mapper.Map(wiki);
        }

        public void NewWiki(string wikiURL, string pageName, string content)
        {
            _db.Wiki.Add(new Models.Wiki()
            {
                PageName = pageName,
                Url = wikiURL
            });
            _db.SaveChanges();
            SetMD(wikiURL, content);
        }

        public void NewWiki(string wikiURL, string content)
        {
            NewWiki(wikiURL, wikiURL, content);
        }

        public void SetMD(string wikiURL, string content)
        {
            SetMD(GetId(wikiURL), content);
        }

        private void SetMD(int wikiId, string content)
        {
            var wikiMD = (from desc in _db.WikiMdDescription
                          where desc.WikiId == wikiId
                          select desc).SingleOrDefault();
            if (wikiMD != null)
            {
                wikiMD.MdDescription = content;
            } else
            {
                _db.WikiMdDescription.Add(new WikiMdDescription()
                {
                    WikiId = wikiId,
                    MdDescription = content
                });
            }
            _db.SaveChanges();
        }

        public void SetName(string wikiURL, string newName)
        {
            SetName(GetId(wikiURL), newName);
        }

        private void SetName(int wikiId, string newName)
        {
            Models.Wiki wiki = (from inWiki in _db.Wiki
                                where inWiki.Id == wikiId
                                select inWiki).Single();
            wiki.PageName = newName;
            _db.SaveChanges();
        }
    }
}
