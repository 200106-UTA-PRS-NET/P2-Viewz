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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var wrepo = new WikiRepositoryStoring(context, factory);
            var prepo = new PageRepositoryStoring(context, factory);
            var wiki_controller = new WikiController(wrepo, prepo, w_logger);
            var wiki = new ViewzApi.Models.Wiki()
            {
                PageName = "I'm Hungry",
                Description = "This is a description"
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
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
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
