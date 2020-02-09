using DataAccess.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
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
            select page.PageId).First();
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

        public void SetHTML(string wikiURL, string pageURL, string content)
        {
            SetHTML(GetID(wikiURL, pageURL), content);
        }

        protected void SetHTML(long pageID, string content)
        {
            // TODO put in table split classes
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
            // TODO put in table split classes
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

        private protected void SetDetails(long pageID, IEnumerable<DataAccess.Storing.Contents> contents)
        {
            throw new NotImplementedException(); // TODO
        }
    }
}
