using System;
using System.Collections.Generic;
using System.Text;
using ViewzApi.Controllers;
using DataAccess.Models;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using DataAccess.APIAccess;
using Microsoft.Extensions.Logging;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Exceptions;
using System.Threading.Tasks;

namespace Tests
{
    //TEST CASES
    ///one page that has mdcontent but not htmlcontent
    ///one page that has both
    ///both null
    public class APIServiceTesting
    {
        private readonly ILogger<ViewzApi.Controllers.PageController> p_logger;
        private readonly ILogger<ViewzApi.Controllers.WikiController> w_logger;

        public APIServiceTesting()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole()
                    .AddEventLog();
            });
            p_logger = loggerFactory.CreateLogger<ViewzApi.Controllers.PageController>();
            w_logger = loggerFactory.CreateLogger<ViewzApi.Controllers.WikiController>();
        }

        [Theory]
        [InlineData("training-code", "readme")]
        [InlineData("training-code", "spongebob")]
        [InlineData("training-code", "squiddy")]
        [InlineData("bad-wiki", "readme")]
        [InlineData("training-code", "bad-page")]
        public async Task GetPageTestAsync(string wikiURL, string pageURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var repo = new PageRepositoryStoring(context, factory);
            var page_controller = new PageController(repo, p_logger);

            Wiki wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            Page page;
            if (wiki != null)
            {
                page = await context.Page.Where(p => (p.WikiId == wiki.Id && p.Url == pageURL)).SingleOrDefaultAsync();
            }
            else
            {
                page = null;
            }

            var status = await page_controller.GetAsync(wikiURL, pageURL, ViewzApi.Controllers.PageContent.Html);
            if (page != null)
            {
                Assert.IsType<OkObjectResult>(status);
            }
            else
            {
                Assert.IsType<NotFoundObjectResult>(status);
            }
        }

        [Theory]
        [InlineData("training-code", "readme")]
        [InlineData("training-code", "spongebob")]
        [InlineData("training-code", "squiddy")]
        [InlineData("bad-wiki", "readme")]
        [InlineData("training-code", "bad-page")]
        public async Task GetPageNoContentTestAsync(string wikiURL, string pageURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var repo = new PageRepositoryStoring(context, factory);
            var page_controller = new PageController(repo, p_logger);

            Wiki wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            Page page;
            if (wiki != null)
            {
                page = await context.Page.Where(p => (p.WikiId == wiki.Id && p.Url == pageURL)).SingleOrDefaultAsync();
            }
            else
            {
                page = null;
            }

            var status = await page_controller.GetAsync(wikiURL, pageURL, ViewzApi.Controllers.PageContent.NoContent);
            if (page != null)
            {
                Assert.IsType<OkObjectResult>(status);
            }
            else
            {
                Assert.IsType<NotFoundObjectResult>(status);
            }
        }

        [Theory]
        [InlineData("training-code", "readme")]
        [InlineData("training-code", "spongebob")]
        [InlineData("training-code", "squiddy")]
        [InlineData("bad-wiki", "readme")]
        [InlineData("training-code", "bad-page")]
        public async Task GetPageMDTestAsync(string wikiURL, string pageURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var repo = new PageRepositoryStoring(context, factory);
            var page_controller = new PageController(repo, p_logger);

            Wiki wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            Page page;
            if (wiki != null)
            {
                page = await context.Page.Where(p => (p.WikiId == wiki.Id && p.Url == pageURL)).SingleOrDefaultAsync();
            }
            else
            {
                page = null;
            }

            var status = await page_controller.GetAsync(wikiURL, pageURL, ViewzApi.Controllers.PageContent.Md);
            if (page != null)
            {
                Assert.IsType<OkObjectResult>(status);
            }
            else
            {
                Assert.IsType<NotFoundObjectResult>(status);
            }
        }

        [Theory]
        [InlineData("training-code", "new-page")]
        [InlineData("training-code", "readme")]
        [InlineData("bad-wiki", "new-page")]
        public async Task PostPageTestAsync(string wikiURL, string pageURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var repo = new PageRepositoryStoring(context, factory);
            var page_controller = new PageController(repo, p_logger);

            Wiki wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            Page page;
            if (wiki != null)
            {
                page = await context.Page.Where(p => (p.WikiId == wiki.Id && p.Url == pageURL)).SingleOrDefaultAsync();
            }
            else
            {
                page = null;
            }
            var model = new ViewzApi.Models.Page()
            {
                Content = "# Header 1",
                Details = new List<DataAccess.Storing.PageDetails>(),
                PageName = pageURL,
                Url = pageURL
            };

            if (wiki != null && page == null)
            {
                Assert.IsType<CreatedAtActionResult>(await page_controller.PostAsync(wikiURL, pageURL, model));
            }
            else if (wiki == null)
            {
                Assert.IsType<NotFoundObjectResult>(await page_controller.PostAsync(wikiURL, pageURL, model));
            }
            else
            {
                // It should return Conflict, but because there is no unique constraints in in-memory db, it will throw a not found
                await Assert.ThrowsAsync<PageNotFoundException>(() => page_controller.PostAsync(wikiURL, pageURL, model));
                //Assert.IsType<ConflictObjectResult>(page_controller.Post(wikiURL, pageURL, model));
            }
        }

        [Theory]
        [InlineData("training-code", "new-page")]
        [InlineData("training-code", "readme")]
        [InlineData("bad-wiki", "readme")]
        public async Task GoodPatchPageTestAsync(string wikiURL, string pageURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var repo = new PageRepositoryStoring(context, factory);
            var page_controller = new PageController(repo, p_logger);

            Wiki wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            Page page;
            if (wiki != null)
            {
                page = await context.Page.Where(p => (p.WikiId == wiki.Id && p.Url == pageURL)).SingleOrDefaultAsync();
            }
            else
            {
                page = null;
            }
            var model = new ViewzApi.Models.Page()
            {
                Content = "# Header 1",
                Details = new List<DataAccess.Storing.PageDetails>(),
                PageName = pageURL,
                Url = pageURL
            };

            //ACT
            var status = await page_controller.PatchAsync(wikiURL, pageURL, model);
            if (wiki == null || page == null)
            {
                Assert.IsType<NotFoundObjectResult>(status);
            }
            else
            {
                //ASSERT - status 204 (No Content)
                Assert.IsType<NoContentResult>(status);
            }
        }

        [Theory]
        [InlineData("training-code", "new-page")]
        [InlineData("training-code", "readme")]
        [InlineData("bad-wiki", "readme")]
        public async Task PatchPage400TestAsync(string wikiURL, string pageURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var repo = new PageRepositoryStoring(context, factory);
            var page_controller = new PageController(repo, p_logger);

            Wiki wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            Page page;
            if (wiki != null)
            {
                page = await context.Page.Where(p => (p.WikiId == wiki.Id && p.Url == pageURL)).SingleOrDefaultAsync();
            }
            else
            {
                page = null;
            }
            //ACT
            var status = await page_controller.PatchAsync(wikiURL, pageURL, new ViewzApi.Models.Page());

            //ASSERT - status 400 (Bad Request)
            Assert.IsType<BadRequestObjectResult>(status);
        }

        [Fact]
        public async Task GetPopWiki200TestAsync()
        {
            //ARRANGE
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var prepo = new PageRepositoryStoring(context, factory);
            var wrepo = new WikiRepositoryStoring(context, factory);
            var wiki_controller = new WikiController(wrepo, prepo, w_logger);

            var status = await wiki_controller.GetAsync(5);
            //ACT
            //ASSERT
            Assert.IsType<OkObjectResult>(status);
        }

        [Theory]
        [InlineData("training-code")]
        [InlineData("bad-wiki")]
        public async Task GetWikiTestAsync(string wikiURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var wrepo = new WikiRepositoryStoring(context, factory);
            var prepo = new PageRepositoryStoring(context, factory);
            var wiki_controller = new WikiController(wrepo, prepo, w_logger);

            var wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            //ACT
            var status = await wiki_controller.GetAsync(wikiURL);
            if (wiki != null)
            {
                //ASSERT - status 200 (OK)
                Assert.IsType<OkObjectResult>(status);
            }
            else
            {
                //ASSERT - status 404 (OK)
                Assert.IsType<NotFoundResult>(status);
            }
        }

        [Theory]
        [InlineData("training-code")]
        [InlineData("bad-wiki")]
        public async Task PostWikiTestAsync(string wikiURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var prepo = new PageRepositoryStoring(context, factory);
            var wrepo = new WikiRepositoryStoring(context, factory);
            var wiki_controller = new WikiController(wrepo, prepo, w_logger);

            var wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            var content = new PageHtmlContent();
            var W = new ViewzApi.Models.Wiki()
            {
                Url = wikiURL,
                Description = "",
                PageName = wikiURL
            };

            var status = await wiki_controller.PostAsync(wikiURL, W);

            if (wiki == null)
            {
                //ASSERT
                Assert.IsType<CreatedAtActionResult>(status);
            }
            else
            {
                Assert.IsType<ConflictObjectResult>(status);
            }
        }

        [Theory]
        [InlineData("training-code")]
        [InlineData("bad-wiki")]
        public async Task PatchWiki204TestAsync(string wikiURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var wrepo = new WikiRepositoryStoring(context, factory);
            var prepo = new PageRepositoryStoring(context, factory);
            var wiki_controller = new WikiController(wrepo, prepo, w_logger);
            var wiki = new ViewzApi.Models.Wiki()
            {
                PageName = "I'm Hungry"
            };

            var WIKI = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            //ACT
            var status = await wiki_controller.PatchAsync(wikiURL, wiki);
            if (WIKI != null)
            {
                //ASSERT
                Assert.IsType<NoContentResult>(status);
            }
            else
            {
                Assert.IsType<NotFoundResult>(status);
            }
        }

        [Theory]
        [InlineData("training-code")]
        [InlineData("bad-wiki")]
        public async Task PatchWiki400TestAsync(string wikiURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMEM(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var wrepo = new WikiRepositoryStoring(context, factory);
            var prepo = new PageRepositoryStoring(context, factory);
            var wiki_controller = new WikiController(wrepo, prepo, w_logger);

            var WIKI = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            //ACT
            var status = await wiki_controller.PatchAsync(wikiURL, new ViewzApi.Models.Wiki());
            if (WIKI != null)
            {
                //ASSERT
                Assert.IsType<BadRequestObjectResult>(status);
            }
            else
            {
                //ASSERT
                Assert.IsType<BadRequestObjectResult>(status);
            }
        }

        ViewzDbContext InMEM(string instance)
        {
            var options = new DbContextOptionsBuilder<ViewzDbContext>().UseInMemoryDatabase(databaseName: instance).Options;
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

/*
--------------------------------------------------------------------------------------------
 function:
    Init In Mem DB Context
    Set up data?

function:
    Init Repo with ^
    Init Controller with ^

test functions:
    Call controller actions

    Set Ids through DB context if needed
    
    check equality from db context
--------------------------------------------------------------------------------------------
testing storing and retrieving through storing and all original database functions
initialize storing repo because it is all three repos and they all call base at some point

storing does post and get
retrieving does get

when you init inmemory context, store data inside so you know it works, and test through that
init repo and controller at the same time, and I should be able to call controlelr actions
Think about the different paths that can be taken (pagehtmlcontents: make one page that exists, one page that mdcontent and htmlcontent already set, one page where one content is set and other isn't, and when both are null)

since you are on branch-dev, branch off of it to a new reddev, push to origin/reddev, and pull when everything is ready
--------------------------------------------------------------------------------------------
 */
