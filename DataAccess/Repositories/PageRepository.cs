using DataAccess.Interfaces;
//using System;
using System.Linq;
using System.Collections.Generic;
//using System.Text;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PageRepository : IPageRepository
    {
        readonly ViewzDbContext _db;
        public PageRepository(ViewzDbContext db)
        {
            _db = db;
        }
        public string GetHTML(string wikiURL, string pageURL)
        {
            return GetHTML(GetID(wikiURL, pageURL));
        }

        protected virtual string GetHTML(long pageID)
        {
            return (from contents in _db.PageHtmlContent
                    where contents.PageId == pageID
                    select contents.HtmlContent).Single();
        }

        long GetID(string wikiURL, string pageURL)
        {
            return (from page in _db.Page
            join wikiId in from wiki in _db.Wiki
                           where wikiURL == wiki.Url
                           select wiki.Id
                         on page.WikiId equals wikiId
            where page.Url == pageURL
            select page.PageId).Single();
        }

        int GetID(string wikiURL)
        {
            return (from wiki in _db.Wiki
                    where wiki.Url == wikiURL
                    select wiki.Id).Single();
        }

        public string GetMD(string wikiURL, string pageURL)
        {
            return GetMD(GetID(wikiURL, pageURL));
        }

        protected string GetMD(long pageID)
        {
            return (from contents in _db.PageMdContent
                    where contents.PageId == pageID
                    select contents.MdContent).Single();
        }

        protected void SetHTML(string wikiURL, string pageURL, string content)
        {
            SetHTML(GetID(wikiURL, pageURL), content);
        }

        protected void SetHTML(long pageID, string content)
        {
            //!!! put in table split classes
            var pageHtml = (from contents in _db.PageHtmlContent
                          where contents.PageId == pageID
                          select contents).SingleOrDefault();

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
            _db.SaveChanges();
        }

        public void SetMD(string wikiURL, string pageURL, string content)
        {
            SetMD(GetID(wikiURL, pageURL), content);
        }

        protected virtual void SetMD(long pageID, string content)
        {
            //!!! put in table split classes
            var pageMD = (from contents in _db.PageMdContent
                          where contents.PageId == pageID
                          select contents).SingleOrDefault();
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
            _db.SaveChanges();
        }

        private protected void SetContents(long pageID, IEnumerable<DataAccess.Storing.Contents> contents)
        {
            (from innerPage in _db.Page
                        where innerPage.PageId == pageID
                        select innerPage)
                        .Include(o => o.Contents).Single()
                        .Contents = Storing.Mapper.Map(contents);
            _db.SaveChanges();
        }

        public void NewPage(string wikiURL, string pageURL, string content)
        {
            NewPage(wikiURL, pageURL, pageURL, content);
        }

        public void NewPage(string wikiURL, string pageURL, string pageName, string content)
        {
            int wikiId = GetID(wikiURL);
            _db.Page.Add(new Models.Page()
            {
                WikiId = wikiId,
                Url = pageURL,
                PageName = pageName
            });
            _db.SaveChanges();
            SetMD(wikiURL, pageURL, content);
        }

        public Storing.Page GetPage(string wikiURL, string pageURL)
        {
            return GetPage(GetID(wikiURL, pageURL));
        }
        protected Storing.Page GetPage(long pageID)
        {
            var page = (from innerPage in _db.Page
                        where innerPage.PageId == pageID
                        select innerPage)
                        .Include(o => o.PageDetails)
                        .Include(o => o.Contents).Single();
            page.HitCount += 1;
            _db.SaveChanges();
            return Storing.Mapper.Map(page);
        }

        public Storing.Page GetPageWithMD(string wikiURL, string pageURL)
        {
            return GetPageWithMD(GetID(wikiURL, pageURL));
        }
        protected Storing.Page GetPageWithMD(long pageID)
        {
            var page = (from innerPage in _db.Page
                        where innerPage.PageId == pageID
                        select innerPage)
                        .Include(o => o.PageDetails)
                        .Include(o => o.Contents)
                        .Include(o => o.PageMdContent).Single();
            page.HitCount += 1;
            _db.SaveChanges();
            return Storing.Mapper.Map(page);
        }

        public Storing.Page GetPageWithHTML(string wikiURL, string pageURL)
        {
            return GetPageWithHTML(GetID(wikiURL, pageURL));
        }
        protected Storing.Page GetPageWithHTML(long pageID)
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
            page.HtmlContent = GetHTML(pageID);
            page.Contents = (from content in _db.Contents
                             where content.PageId == pageID
                             orderby content.Order ascending
                             select Storing.Mapper.Map(content)).Skip(1);
            }
            dbSave.Wait();
            return page;
        }

        public IEnumerable<Storing.Page> GetPopularPages(string wikiURL, uint count = 5)
        {
            return GetPopularPages(GetID(wikiURL), count);
        }
        protected IEnumerable<Storing.Page> GetPopularPages(int wikiID, uint count = 5)
        {
            IEnumerable<Models.Page> pages = (from page in _db.Page
                    where page.WikiId == wikiID
                    orderby page.HitCount descending
                    select page)
                    .Include(o => o.PageDetails)
                    .Include(o => o.Contents)
                    .Take((int)count).ToList();
            foreach(var page in pages)
            {
                //SMELL#31,32: replaced page.Contents.Count == 0 with !page.Contents.Any()
                if (!page.Contents.Any())
                {
                    GetHTML(page.PageId); // Will either create the page or just load html from db
                    page.Contents = (from content in _db.Contents
                                     where content.PageId == page.PageId
                                     orderby content.Order ascending
                                     select content).ToList();
                }
            }
            return from page in pages
                   select Storing.Mapper.Map(page);
        }

        public void SetName(string wikiURL, string pageURL, string newName)
        {
            SetName(GetID(wikiURL, pageURL), newName);
        }
        protected void SetName(long pageID, string newName)
        {
            Models.Page page = (from innerPage in _db.Page
                                                    where innerPage.PageId == pageID
                                                    select innerPage).Single();
            page.PageName = newName;
            _db.SaveChanges();
        }

        public void SetPageDetails(string wikiURL, string pageURL, IEnumerable<Storing.PageDetails> details)
        {
            SetPageDetails(GetID(wikiURL, pageURL), details);
        }
        protected void SetPageDetails(long pageID, IEnumerable<Storing.PageDetails> details)
        {
            (from innerPage in _db.Page
             where innerPage.PageId == pageID
             select innerPage)
                        .Include(o => o.PageDetails).Single()
                        .PageDetails = Storing.Mapper.Map(details);
            _db.SaveChanges();
        }
    }
}
