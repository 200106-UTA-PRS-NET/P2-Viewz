using System.Collections.Generic;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Storing;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using DataAccess.Exceptions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class WikiRepository : IWikiRepository
    {
        readonly ViewzDbContext _db;
        public WikiRepository(ViewzDbContext db)
        {
            _db = db;
        }
        async Task<int> GetIdAsync(string wikiURL)
        {
            try {
            return await (from wiki in _db.Wiki
                    where wiki.Url == wikiURL
                    select wiki.Id).SingleAsync();
            }
            catch (InvalidOperationException e)
            {
                throw new WikiNotFoundException($"{wikiURL} not found", e);
            }
        }
        public async Task<string> GetHTMLAsync(string wikiURL)
        {
            return await GetHTMLAsync(await GetIdAsync(wikiURL));
        }

        protected async virtual Task<string> GetHTMLAsync(int wikiId)
        {
            return await (from content in _db.WikiHtmlDescription
                    where content.WikiId == wikiId
                    select content.HtmlDescription).FirstAsync();
        }

        public async Task<string> GetMDAsync(string wikiURL)
        {
            return await GetMDAsync(await GetIdAsync(wikiURL));
        }

        protected async Task<string> GetMDAsync(int wikiId)
        {
            return await (from content in _db.WikiMdDescription
                    where content.WikiId == wikiId
                    select content.MdDescription).SingleAsync();
        }

        public async Task<IEnumerable<Storing.Wiki>> GetPopularWikisAsync(uint count, bool description = false)
        {
            IEnumerable<Models.Wiki> wikis = await (from wiki in _db.Wiki
                         orderby wiki.HitCount descending
                         select wiki)
                        .Take((int)count).ToListAsync();
            return await Task.WhenAll(wikis.Select(async w =>
            {
                Storing.Wiki wiki = Mapper.Map(w);
                wiki.HtmlDescription = (description) ? await GetHTMLAsync(w.Id) : null;
                return wiki;
            }));

        }

        public async Task<Storing.Wiki> GetWikiAsync(string wikiURL)
        {
            return await GetWikiAsync(await GetIdAsync(wikiURL));
        }

        private async Task<Storing.Wiki> GetWikiAsync(int wikiId)
        {
            var wiki = await (from inWiki in _db.Wiki
                               where inWiki.Id == wikiId
                               select inWiki).SingleAsync();
            wiki.HitCount += 1;
            await _db.SaveChangesAsync();
            return Mapper.Map(wiki);
        }

        public async Task<Storing.Wiki> GetWikiWithHTMLAsync(string wikiURL)
        {
            return await GetWikiWithHTMLAsync(await GetIdAsync(wikiURL));
        }

        private async Task<Storing.Wiki> GetWikiWithHTMLAsync(int wikiId)
        {
            var modelsWiki = await (from inwiki in _db.Wiki
                              where inwiki.Id == wikiId
                              select inwiki)
                              .Include(o => o.WikiHtmlDescription)
                              .SingleAsync();
            modelsWiki.HitCount += 1;
            var dbSave = _db.SaveChangesAsync();
            Storing.Wiki wiki = Mapper.Map(modelsWiki);
            if (wiki.HtmlDescription == null)
            {
                wiki.HtmlDescription = await GetHTMLAsync(wikiId);
            }
            await dbSave;
            return wiki;
        }

        public async Task<Storing.Wiki> GetWikiWithMDAsync(string wikiURL)
        {
            return await GetWikiWithMDAsync(await GetIdAsync(wikiURL));
        }

        private async Task<Storing.Wiki> GetWikiWithMDAsync(int wikiId)
        {
            var wiki = await (from inWiki in _db.Wiki
                        where inWiki.Id == wikiId
                        select inWiki)
                        .Include(o => o.WikiMdDescription)
                        .SingleAsync();
            wiki.HitCount += 1;
            await _db.SaveChangesAsync();
            return Mapper.Map(wiki);
        }

        public async Task NewWikiAsync(string wikiURL, string pageName, string content)
        {
            _db.Wiki.Add(new Models.Wiki()
            {
                PageName = pageName,
                Url = wikiURL
            });
            await _db.SaveChangesAsync();
            await SetMDAsync(wikiURL, content);
        }

        public async Task NewWikiAsync(string wikiURL, string content)
        {
            await NewWikiAsync(wikiURL, wikiURL, content);
        }

        public async Task SetMDAsync(string wikiURL, string content)
        {
            await SetMDAsync(await GetIdAsync(wikiURL), content);
        }

        protected async virtual Task SetMDAsync(int wikiId, string content)
        {
            var wikiMD = await (from desc in _db.WikiMdDescription
                          where desc.WikiId == wikiId
                          select desc).SingleOrDefaultAsync();
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
            await _db.SaveChangesAsync();
        }

        public async Task SetNameAsync(string wikiURL, string newName)
        {
            await SetNameAsync(await GetIdAsync(wikiURL), newName);
        }

        private async Task SetNameAsync(int wikiId, string newName)
        {
            Models.Wiki wiki = await (from inWiki in _db.Wiki
                                where inWiki.Id == wikiId
                                select inWiki).SingleAsync();
            wiki.PageName = newName;
            await _db.SaveChangesAsync();
        }

        protected async Task SetHTMLAsync(string wikiURL, string content)
        {
            await SetHTMLAsync(await GetIdAsync(wikiURL), content);
        }

        protected async Task SetHTMLAsync(int wikiId, string content)
        {
            var pageHtml = await (from contents in _db.WikiHtmlDescription
                            where contents.WikiId == wikiId
                            select contents).SingleOrDefaultAsync();
            if (pageHtml != null)
            {
                pageHtml.HtmlDescription = content;
            }
            else
            {
                _db.WikiHtmlDescription.Add(new WikiHtmlDescription()
                {
                    WikiId = wikiId,
                    HtmlDescription = content
                });
            }
            await _db.SaveChangesAsync();
        }
    }
}
