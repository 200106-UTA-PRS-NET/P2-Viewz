using DataAccess.Interfaces;
using System.Linq;
using System.Collections.Generic;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using DataAccess.Exceptions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PageRepository : IPageRepository
    {
        readonly ViewzDbContext _db;
        public PageRepository(ViewzDbContext db)
        {
            _db = db;
        }
        public async Task<string> GetHTMLAsync(string wikiURL, string pageURL)
        {
            return await GetHTMLAsync(await GetIDAsync(wikiURL, pageURL));
        }

        protected async virtual Task<string> GetHTMLAsync(long pageID)
        {
            return await (from contents in _db.PageHtmlContent
                    where contents.PageId == pageID
                    select contents.HtmlContent).SingleAsync();
        }

        async Task<long> GetIDAsync(string wikiURL, string pageURL)
        {
            try
            {
                return await (from page in _db.Page
                        join wikiId in from wiki in _db.Wiki
                                       where wikiURL == wiki.Url
                                       select wiki.Id
                                     on page.WikiId equals wikiId
                        where page.Url == pageURL
                        select page.PageId).SingleAsync();
            } catch (InvalidOperationException e)
            {
                throw new PageNotFoundException($"{wikiURL}/{pageURL} not found", e);
            }
        }

        async Task<int> GetIDAsync(string wikiURL)
        {
            try {
            return await (from wiki in _db.Wiki
                    where wiki.Url == wikiURL
                    select wiki.Id).SingleAsync();
            } catch (InvalidOperationException e)
            {
                throw new WikiNotFoundException($"{wikiURL} was not found.", e);
            }
        }

        public async Task<string> GetMDAsync(string wikiURL, string pageURL)
        {
            return await GetMDAsync(await GetIDAsync(wikiURL, pageURL));
        }

        protected async Task<string> GetMDAsync(long pageID)
        {
            return await (from contents in _db.PageMdContent
                    where contents.PageId == pageID
                    select contents.MdContent).SingleAsync();
        }

        protected async Task SetHTMLAsync(string wikiURL, string pageURL, string content)
        {
            await SetHTMLAsync(await GetIDAsync(wikiURL, pageURL), content);
        }

        protected async Task SetHTMLAsync(long pageID, string content)
        {
            //!!! put in table split classes
            var pageHtml = await (from contents in _db.PageHtmlContent
                          where contents.PageId == pageID
                          select contents).SingleOrDefaultAsync();

            if (pageHtml != null)
            {
                pageHtml.HtmlContent = content;
            }
            else
            {
                _db.PageHtmlContent.Add(new PageHtmlContent()
                {
                    PageId = pageID,
                    HtmlContent = content
                });
            }
            await _db.SaveChangesAsync();
        }

        public async Task SetMDAsync(string wikiURL, string pageURL, string content)
        {
            await SetMDAsync(await GetIDAsync(wikiURL, pageURL), content);
        }

        protected virtual async Task SetMDAsync(long pageID, string content)
        {
            //!!! put in table split classes
            var pageMD = await (from contents in _db.PageMdContent
                          where contents.PageId == pageID
                          select contents).SingleOrDefaultAsync();
            if (pageMD != null)
            {
                pageMD.MdContent = content;
            }
            else
            {
                _db.PageMdContent.Add(new PageMdContent()
                {
                    PageId = pageID,
                    MdContent = content
                });
            }
            await _db.SaveChangesAsync();
        }

        private protected async Task SetContentsAsync(long pageID, IEnumerable<DataAccess.Storing.Contents> contents)
        {
            (await (from innerPage in _db.Page
                        where innerPage.PageId == pageID
                        select innerPage)
                        .Include(o => o.Contents).SingleAsync())
                        .Contents = Storing.Mapper.Map(contents);
            await _db.SaveChangesAsync();
        }

        public async Task NewPageAsync(string wikiURL, string pageURL, string content)
        {
            await NewPageAsync(wikiURL, pageURL, pageURL, content);
        }

        public async Task NewPageAsync(string wikiURL, string pageURL, string pageName, string content)
        {
            int wikiId = await GetIDAsync(wikiURL);
            try
            {
                _db.Page.Add(new Models.Page()
                {
                    WikiId = wikiId,
                    Url = pageURL,
                    PageName = pageName
                });
                await _db.SaveChangesAsync();
            } catch(Exception e)
            {
                throw new PageExistsException($"{wikiURL}/{pageURL} already exists", e);
            }
            await SetMDAsync(wikiURL, pageURL, content);
        }

        public async Task<Storing.Page> GetPageAsync(string wikiURL, string pageURL)
        {
            return await GetPageAsync(await GetIDAsync(wikiURL, pageURL));
        }
        protected async Task<Storing.Page> GetPageAsync(long pageID)
        {
            var page = await (from innerPage in _db.Page
                        where innerPage.PageId == pageID
                        select innerPage)
                        .Include(o => o.PageDetails)
                        .Include(o => o.Contents).SingleAsync();
            page.HitCount += 1;
            await _db.SaveChangesAsync();
            return Storing.Mapper.Map(page);
        }

        public async Task<Storing.Page> GetPageWithMDAsync(string wikiURL, string pageURL)
        {
            return await GetPageWithMDAsync(await GetIDAsync(wikiURL, pageURL));
        }
        protected async Task<Storing.Page> GetPageWithMDAsync(long pageID)
        {
            var page = await (from innerPage in _db.Page
                        where innerPage.PageId == pageID
                        select innerPage)
                        .Include(o => o.PageDetails)
                        .Include(o => o.Contents)
                        .Include(o => o.PageMdContent).SingleAsync();
            page.HitCount += 1;
            await _db.SaveChangesAsync();
            return Storing.Mapper.Map(page);
        }

        public async Task<Storing.Page> GetPageWithHTMLAsync(string wikiURL, string pageURL)
        {
            return await GetPageWithHTMLAsync(await GetIDAsync(wikiURL, pageURL));
        }
        protected async Task<Storing.Page> GetPageWithHTMLAsync(long pageID)
        {
            var modelsPage = (from innerPage in _db.Page
                        where innerPage.PageId == pageID
                        select innerPage)
            .Include(o => o.PageDetails)
            .Include(o => o.Contents)
            .Include(o => o.PageHtmlContent).Single();
            modelsPage.HitCount += 1;
            var dbSave = _db.SaveChangesAsync();
            Storing.Page page = Storing.Mapper.Map(modelsPage);
            if (page.HtmlContent == null) { 
            page.HtmlContent = await GetHTMLAsync(pageID);
            page.Contents = (from content in _db.Contents
                             where content.PageId == pageID
                             orderby content.Order ascending
                             select Storing.Mapper.Map(content)).Skip(1);
            }
            await dbSave;
            return page;
        }

        public async Task<IEnumerable<Storing.Page>> GetPopularPagesAsync(string wikiURL, uint count = 5)
        {
            return await GetPopularPagesAsync(await GetIDAsync(wikiURL), count);
        }
        protected async Task<IEnumerable<Storing.Page>> GetPopularPagesAsync(int wikiID, uint count = 5)
        {
            IEnumerable<Models.Page> pages = await (from page in _db.Page
                    where page.WikiId == wikiID
                    orderby page.HitCount descending
                    select page)
                    .Include(o => o.PageDetails)
                    .Include(o => o.Contents)
                    .Take((int)count).ToListAsync();
            foreach(var page in pages)
            {
                //SMELL#31,32: replaced page.Contents.Count == 0 with !page.Contents.Any()
                if (!page.Contents.Any())
                {
                    await GetHTMLAsync(page.PageId); // Will either create the page or just load html from db
                    page.Contents = await (from content in _db.Contents
                                     where content.PageId == page.PageId
                                     orderby content.Order ascending
                                     select content).Skip(1).ToListAsync();
                }
            }
            return from page in pages
                   select Storing.Mapper.Map(page);
        }

        public async Task SetNameAsync(string wikiURL, string pageURL, string newName)
        {
            await SetNameAsync(await GetIDAsync(wikiURL, pageURL), newName);
        }
        protected async Task SetNameAsync(long pageID, string newName)
        {
            Models.Page page = await (from innerPage in _db.Page
                                                    where innerPage.PageId == pageID
                                                    select innerPage).SingleAsync();
            page.PageName = newName;
            await _db.SaveChangesAsync();
        }

        public async Task SetPageDetailsAsync(string wikiURL, string pageURL, IEnumerable<Storing.PageDetails> details)
        {
            await SetPageDetailsAsync(await GetIDAsync(wikiURL, pageURL), details);
        }
        protected async Task SetPageDetailsAsync(long pageID, IEnumerable<Storing.PageDetails> details)
        {
            (await (from innerPage in _db.Page
             where innerPage.PageId == pageID
             select innerPage)
                        .Include(o => o.PageDetails).SingleAsync())
                        .PageDetails = Storing.Mapper.Map(details);
            await _db.SaveChangesAsync();
        }
    }
}
