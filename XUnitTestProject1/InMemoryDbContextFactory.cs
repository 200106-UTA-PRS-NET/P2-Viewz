using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    static class InMemoryDbContextFactory
    {
        public static ViewzDbContext GetViewzDbContext(string instanceName)
        {
            var options = new DbContextOptionsBuilder<ViewzDbContext>().UseInMemoryDatabase(databaseName: instanceName).Options;
            var context = new ViewzDbContext(options);

            //////ADD 1 WIKI////////////////////
            var wiki = new Wiki()
            {
                Id = 1,
                Url = "training-code",
                PageName = "Training Code",
                HitCount = 100,
                Page = new List<Page>(),
                WikiHtmlDescription = new WikiHtmlDescription(),
                WikiMdDescription = new WikiMdDescription()
            };
            ///////////////////////////////////////////////

            ///FILL WIKI BEGIN///
            var wikihtmldescription = new WikiHtmlDescription()
            {
                WikiId = wiki.Id,
                Wiki = wiki,
                HtmlDescription = "<h1>This is a description AND a header</h1>"
            };
            wiki.WikiHtmlDescription = wikihtmldescription;

            var wikimddescription = new WikiMdDescription()
            {
                WikiId = wiki.Id,
                Wiki = wiki,
                MdDescription = "# This is a description AND a header"
            };
            wiki.WikiMdDescription = wikimddescription;
            //////////////////////////////////////////////
            ///////////////ADD 3 PAGES///////////////
            var page = new Page()
            {
                WikiId = wiki.Id,
                PageId = 1,
                Url = "readme",
                PageName = "Read Me",
                HitCount = 0,
                Wiki = wiki,
                PageHtmlContent = new PageHtmlContent(),
                PageMdContent = new PageMdContent(),
                Contents = new List<Contents>(),
                PageDetails = new List<PageDetails>()
            };
            wiki.Page.Add(page);

            var page2 = new Page()
            {
                WikiId = wiki.Id,
                PageId = 2,
                Url = "spongebob",
                PageName = "Square Pants",
                HitCount = 100,
                Wiki = wiki,
                PageHtmlContent = new PageHtmlContent(),
                PageMdContent = null,
                Contents = new List<Contents>(),
                PageDetails = new List<PageDetails>()
            };
            wiki.Page.Add(page2);

            var page3 = new Page()
            {
                WikiId = wiki.Id,
                PageId = 3,
                Url = "squiddy",
                PageName = "Squidy Page",
                HitCount = 2,
                Wiki = wiki,
                PageHtmlContent = null,
                PageMdContent = null,
                Contents = new List<Contents>(),
                PageDetails = new List<PageDetails>()
            };
            wiki.Page.Add(page3);
            //////////////////////////////////////////////
            ///FILL WIKI END///

            ///FILL PAGES BEGIN///
            /////////////ADD HTMLCONTENTS/////////////
            var pagehtmlcontent = new PageHtmlContent()
            {
                HtmlContent = "<h3>This is a head3</h3>",
                PageId = page.PageId,
                Page = page
            };
            page.PageHtmlContent = pagehtmlcontent;

            var pagehtmlcontent2 = new PageHtmlContent()
            {
                HtmlContent = "<h6>Continuation into h6</h6>",
                PageId = page2.PageId,
                Page = page2
            };
            page2.PageHtmlContent = pagehtmlcontent2;

            var pagehtmlcontent3 = new PageHtmlContent()
            {
                HtmlContent = "<a href=\"vhat\">This is an empty link</a>",
                PageId = page3.PageId,
                Page = page3
            };
            page3.PageHtmlContent = pagehtmlcontent3;
            //////////////////////////////////////////////
            /////////////ADD MDCONTENTS/////////////
            var pagemdcontent = new PageMdContent()
            {
                MdContent = "### This is a head3",
                PageId = page.PageId,
                Page = page
            };
            page.PageMdContent = pagemdcontent;

            var pagemdcontent2 = new PageMdContent()
            {
                MdContent = "###### Continuation into h6",
                PageId = page2.PageId,
                Page = page2
            };
            page2.PageMdContent = pagemdcontent2;

            var pagemdcontent3 = new PageMdContent()
            {
                MdContent = "[This is an empty link](vhat)",
                PageId = page3.PageId,
                Page = page3
            };
            page3.PageMdContent = pagemdcontent3;
            /////////////////////////////////////////
            ///////ADD LINKS TO TOC//////////////////
            var content = new Contents()
            {
                Content = "display link 1",
                Id = "anchor-1",
                Level = 1,
                Order = 1,
                Page = page,
                PageId = page.PageId
            };
            page.Contents.Add(content);

            var content2 = new Contents()
            {
                Content = "display link 2",
                Id = "anchor-2",
                Level = 1,
                Order = 2,
                Page = page,
                PageId = page.PageId
            };
            page.Contents.Add(content2);

            var content3 = new Contents()
            {
                Content = "display link 3",
                Id = "anchor-3",
                Level = 2,
                Order = 3,
                Page = page,
                PageId = page.PageId
            };
            page.Contents.Add(content3);

            var content4 = new Contents()
            {
                Content = "display link 1",
                Id = "anchor-1",
                Level = 1,
                Order = 1,
                Page = page2,
                PageId = page2.PageId
            };
            page2.Contents.Add(content4);

            var content5 = new Contents()
            {
                Content = "display link 2",
                Id = "anchor-2",
                Level = 1,
                Order = 2,
                Page = page2,
                PageId = page2.PageId
            };
            page2.Contents.Add(content5);

            var content6 = new Contents()
            {
                Content = "display link 3",
                Id = "anchor-3",
                Level = 2,
                Order = 3,
                Page = page2,
                PageId = page2.PageId
            };
            page2.Contents.Add(content6);

            var content7 = new Contents()
            {
                Content = "display link 1",
                Id = "anchor-1",
                Level = 1,
                Order = 1,
                Page = page3,
                PageId = page3.PageId
            };
            page3.Contents.Add(content7);

            var content8 = new Contents()
            {
                Content = "display link 2",
                Id = "anchor-2",
                Level = 1,
                Order = 2,
                Page = page3,
                PageId = page3.PageId
            };
            page3.Contents.Add(content8);

            var content9 = new Contents()
            {
                Content = "display link 3",
                Id = "anchor-3",
                Level = 2,
                Order = 3,
                Page = page3,
                PageId = page3.PageId
            };
            page3.Contents.Add(content9);
            /////////////////////////////////////
            /////////ADD PAGE DETAILS///////////
            var detail = new PageDetails()
            {
                DetKey = "key1",
                DetValue = "value1",
                Order = 1,
                Page = page,
                PageId = page.PageId
            };
            page.PageDetails.Add(detail);

            var detail2 = new PageDetails()
            {
                DetKey = "key2",
                DetValue = "value2",
                Order = 2,
                Page = page,
                PageId = page.PageId
            };
            page.PageDetails.Add(detail2);

            var detail3 = new PageDetails()
            {
                DetKey = "key3",
                DetValue = "value3",
                Order = 3,
                Page = page,
                PageId = page.PageId
            };
            page.PageDetails.Add(detail3);

            var detail4 = new PageDetails()
            {
                DetKey = "key1",
                DetValue = "value1",
                Order = 1,
                Page = page2,
                PageId = page2.PageId
            };
            page2.PageDetails.Add(detail4);

            var detail5 = new PageDetails()
            {
                DetKey = "key2",
                DetValue = "value2",
                Order = 2,
                Page = page2,
                PageId = page2.PageId
            };
            page2.PageDetails.Add(detail5);

            var detail6 = new PageDetails()
            {
                DetKey = "key3",
                DetValue = "value3",
                Order = 3,
                Page = page2,
                PageId = page2.PageId
            };
            page2.PageDetails.Add(detail6);

            var detail7 = new PageDetails()
            {
                DetKey = "key1",
                DetValue = "value1",
                Order = 1,
                Page = page3,
                PageId = page3.PageId
            };
            page3.PageDetails.Add(detail7);

            var detail8 = new PageDetails()
            {
                DetKey = "key2",
                DetValue = "value2",
                Order = 2,
                Page = page3,
                PageId = page3.PageId
            };
            page3.PageDetails.Add(detail8);

            var detail9 = new PageDetails()
            {
                DetKey = "key3",
                DetValue = "value3",
                Order = 3,
                Page = page3,
                PageId = page3.PageId
            };
            page3.PageDetails.Add(detail9);
            ////////////////////////////////////
            ///FILL PAGE END

            context.Add(wiki);
            context.Add(wikihtmldescription);
            context.Add(wikimddescription);

            context.Add(page);
            context.Add(page2);
            context.Add(page3);
            context.Add(content);
            context.Add(content2);
            context.Add(content3);
            context.Add(content4);
            context.Add(content5);
            context.Add(content6);
            context.Add(content7);
            context.Add(content8);
            context.Add(content9);
            context.Add(detail);
            context.Add(detail2);
            context.Add(detail3);
            context.Add(detail4);
            context.Add(detail5);
            context.Add(detail6);
            context.Add(detail7);
            context.Add(detail8);
            context.Add(detail9);
            context.Add(pagehtmlcontent);
            context.Add(pagehtmlcontent2);
            context.Add(pagehtmlcontent3);
            context.Add(pagemdcontent);
            context.Add(pagemdcontent2);
            context.Add(pagemdcontent3);

            context.SaveChanges();

            return context;
        }
    }
}
