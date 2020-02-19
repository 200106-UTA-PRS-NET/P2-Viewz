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

        [Fact]
        void GetPageTest()
        {
            //ARRANGE
            using (var context = inMEM("GetPTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var repo = new PageRepositoryStoring(context, factory);
                var page_controller = new PageController(repo, p_logger);

                var wiki = new Wiki();
                var page = new Page();
                var content = new PageHtmlContent();

                if((wiki = context.Wiki.Where(w=>w.Url == "training-code").SingleOrDefault()) != null)
                {
                    if((page = context.Page.Where(p=>p.PageId == 1).SingleOrDefault()) != null)
                    {
                        //ACT - proceed with a Get method call from PageController
                        var status = page_controller.Get(wiki.Url, page.Url, ViewzApi.Controllers.PageContent.Html);

                        //ASSERT - status 200 (success)
                        Assert.Equal(StatusCodes.Status200OK.ToString(), status.ToString());
                    }
                }
            }
        }

        [Fact]
        void GetPageTestNull()
        {
            //ARRANGE
            using (var context = inMEM("GetPTestNull"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var repo = new PageRepositoryStoring(context, factory);
                var page_controller = new PageController(repo, p_logger);

                var wiki = new Wiki();
                var page = new Page();
                var content = new PageHtmlContent();

                if ((wiki = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    if ((page = context.Page.Where(p => p.PageId == 2).SingleOrDefault()) != null)
                    {
                        //ACT - proceed with a Get method call from PageController
                        var status = page_controller.Get(wiki.Url, page.Url, ViewzApi.Controllers.PageContent.Html);

                        //ASSERT - status 404 (Not Found)
                        Assert.Equal(StatusCodes.Status404NotFound.ToString(), status.ToString());
                    }
                }
            }
        }

        [Fact]
        void GetPageTestAllNull()
        {
            //ASSIGN
            using (var context = inMEM("GetPTestAllNull"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var repo = new PageRepositoryStoring(context, factory);
                var page_controller = new PageController(repo, p_logger);

                var wiki = new Wiki();
                var page = new Page();
                var content = new PageHtmlContent();

                if ((wiki = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    if ((page = context.Page.Where(p => p.PageId == 3).SingleOrDefault()) != null)
                    {
                        //ACT - proceed with a Get method call from PageController
                        var status = page_controller.Get(wiki.Url, page.Url, ViewzApi.Controllers.PageContent.Html);

                        //ASSERT - status 404 (Not Found)
                        Assert.Equal(StatusCodes.Status404NotFound.ToString(), status.ToString());
                    }
                }
            }
        }

        [Fact]
        void PostPage201Test()
        {
            //ARRANGE
            using(var context = inMEM("PostPTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var repo = new PageRepositoryStoring(context, factory);
                var page_controller = new PageController(repo, p_logger);

                var wiki = new Wiki();
                var page = new Page();
                var content = new PageHtmlContent();
                var model = new ViewzApi.Models.Page();

                if ((wiki = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    if ((page = context.Page.Where(p => p.PageId == 1).SingleOrDefault()) != null)
                    {
                        //ACT
                        var status = page_controller.Post(wiki.Url, page.Url, model);

                        //ASSERT - status 201 (Created)
                        Assert.Equal(StatusCodes.Status201Created.ToString(), status.ToString());
                    }
                }
            }
        }

        [Fact]
        void PostPage409Test()
        {
            //ARRANGE
            using (var context = inMEM("PostPTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var repo = new PageRepositoryStoring(context, factory);
                var page_controller = new PageController(repo, p_logger);

                var wiki = new Wiki();
                var page = new Page();
                var content = new PageHtmlContent();
                var model = new ViewzApi.Models.Page();

                if ((wiki = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    if ((page = context.Page.Where(p => p.PageId == 1).SingleOrDefault()) != null)
                    {
                        model.Content = page.PageMdContent.MdContent;
                        model.PageName = page.PageName;
                        model.Url = page.Url;

                        var details = new List<DataAccess.Storing.PageDetails>();
                        foreach(var pd in page.PageDetails)
                        {
                            var d = new DataAccess.Storing.PageDetails();
                            d.DetKey = pd.DetKey;
                            d.DetValue = pd.DetValue;
                            details.Add(d);
                        }
                        model.Details = details;

                        var contents = new List<DataAccess.Storing.Contents>();
                        foreach(var pc in page.Contents)
                        {
                            var c = new DataAccess.Storing.Contents();
                            c.Content = pc.Content;
                            c.Id = pc.Id;
                            c.Level = pc.Level;
                            contents.Add(c);
                        }
                        model.Contents = contents;

                        //ACT
                        var status = page_controller.Post(wiki.Url, page.Url, model);

                        //ASSERT - status 409 (Conflict)
                        Assert.Equal(StatusCodes.Status409Conflict.ToString(), status.ToString());
                    }
                }
            }
        }

        [Fact]
        void PatchPage204Test()
        {
            //ARRANGE
            using (var context = inMEM("PatchPTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var repo = new PageRepositoryStoring(context, factory);
                var page_controller = new PageController(repo, p_logger);

                var wiki = new Wiki();
                var page = new Page();
                var content = new PageHtmlContent();
                var model = new ViewzApi.Models.Page();

                if ((wiki = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    if ((page = context.Page.Where(p => p.PageId == 1).SingleOrDefault()) != null)
                    {
                        model.PageName = page.PageName;

                        //ACT
                        var status = page_controller.Patch(wiki.Url, page.Url, model);

                        //ASSERT - status 204 (No Content)
                        Assert.Equal(StatusCodes.Status204NoContent.ToString(), status.ToString());
                    }
                }
            }
        }

        [Fact]
        void PatchPage400Test()
        {
            //ARRANGE
            using (var context = inMEM("PatchPTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var repo = new PageRepositoryStoring(context, factory);
                var page_controller = new PageController(repo, p_logger);

                var wiki = new Wiki();
                var page = new Page();
                var content = new PageHtmlContent();

                if ((wiki = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    if ((page = context.Page.Where(p => p.PageId == 1).SingleOrDefault()) != null)
                    {
                        //ACT
                        var status = page_controller.Patch(wiki.Url, page.Url, null);

                        //ASSERT - status 400 (Bad Request)
                        Assert.Equal(StatusCodes.Status400BadRequest.ToString(), status.ToString());
                    }
                }
            }
        }

        [Fact]
        void GetPopWiki200Test()
        {
            //ARRANGE
            using (var context = inMEM("GetPopWTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var wrepo = new WikiRepositoryStoring(context, factory);
                var prepo = new PageRepositoryStoring(context, factory);
                var wiki_controller = new WikiController(wrepo, prepo, w_logger);

                var wiki = new Wiki();
                if ((wiki = context.Wiki.Where(w => w.HitCount == 9).FirstOrDefault()) != null)
                {
                    //ACT
                    var status = wiki_controller.Get((uint)wiki.HitCount);

                    //ASSERT - status 404 (Not Found)
                    Assert.Equal(StatusCodes.Status200OK.ToString(), status.ToString());
                }
            }
        }
        
        
        [Fact]
        void GetPopWiki404Test()
        {
            //ARRANGE
            using (var context = inMEM("GetPopWTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var wrepo = new WikiRepositoryStoring(context, factory);
                var prepo = new PageRepositoryStoring(context, factory);
                var wiki_controller = new WikiController(wrepo, prepo, w_logger);

                var wiki = new Wiki();
                if((wiki = context.Wiki.Where(w=>w.HitCount == 5).FirstOrDefault()) != null)
                {
                    //ACT
                    var status = wiki_controller.Get((uint)wiki.HitCount);

                    //ASSERT - status 404 (Not Found)
                    Assert.Equal(StatusCodes.Status404NotFound.ToString(), status.ToString());
                }
            }
        }
        
        [Fact]
        void GetWiki200Test()
        {
            //ARRANGE
            using (var context = inMEM("GetWTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var wrepo = new WikiRepositoryStoring(context, factory);
                var prepo = new PageRepositoryStoring(context, factory);
                var wiki_controller = new WikiController(wrepo, prepo, w_logger);

                var wiki = new Wiki();
                if ((wiki = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    //ACT
                    var status = wiki_controller.Get(wiki.Url);

                    //ASSERT - status 200 (OK)
                    Assert.Equal(StatusCodes.Status200OK.ToString(), status.ToString());
                }
            }
        }
        

        [Fact]
        void GetWiki404Test()
        {
            //ARRANGE
            using (var context = inMEM("GetWTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var wrepo = new WikiRepositoryStoring(context, factory);
                var prepo = new PageRepositoryStoring(context, factory);
                var wiki_controller = new WikiController(wrepo, prepo, w_logger);

                var wiki = new Wiki();
                if ((wiki = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    //ACT
                    var status = wiki_controller.Get("this is not a url");

                    //ASSERT - status 404 (OK)
                    Assert.Equal(StatusCodes.Status404NotFound.ToString(), status.ToString());
                }   
            }
        }

        [Fact]
        void PostWiki201Test()
        {
            //ARRANGE
            using (var context = inMEM("PostPTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var prepo = new PageRepositoryStoring(context, factory);
                var wrepo = new WikiRepositoryStoring(context, factory);
                var wiki_controller = new WikiController(wrepo, prepo, w_logger);

                var wiki = new Wiki();
                var page = new Page();
                var content = new PageHtmlContent();
                var model = new ViewzApi.Models.Page();

                if ((wiki = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    var W = new ViewzApi.Models.Wiki()
                    {
                        Url = wiki.Url,
                        Description = wiki.WikiMdDescription.MdDescription,
                        PageName = wiki.PageName
                    };
                    
                    //ACT
                    var status = wiki_controller.Post(wiki.Url, W);

                    //ASSERT
                    Assert.Equal(StatusCodes.Status201Created.ToString(), status.ToString());
                }
            }
        }

        [Fact]
        void PostWiki409Test()
        {
            //ARRANGE
            using (var context = inMEM("PostPTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var prepo = new PageRepositoryStoring(context, factory);
                var wrepo = new WikiRepositoryStoring(context, factory);
                var wiki_controller = new WikiController(wrepo, prepo, w_logger);

                var wiki = new Wiki();
                var page = new Page();
                var content = new PageHtmlContent();
                var model = new ViewzApi.Models.Page();

                if ((wiki = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    var W = new ViewzApi.Models.Wiki()
                    {
                        Url = wiki.Url,
                        Description = wiki.WikiHtmlDescription.HtmlDescription,
                        PageName = wiki.PageName
                    };

                    //ACT
                    var status = wiki_controller.Post(wiki.Url, W);

                    //ASSERT
                    Assert.Equal(StatusCodes.Status409Conflict.ToString(), status.ToString());
                }
            }
        }

        [Fact]
        void PatchWiki204Test()
        {
            //ARRANGE
            using (var context = inMEM("PatchWTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var wrepo = new WikiRepositoryStoring(context, factory);
                var prepo = new PageRepositoryStoring(context, factory);
                var wiki_controller = new WikiController(wrepo, prepo, w_logger);
                var wiki = new ViewzApi.Models.Wiki()
                {
                    PageName = "I'm Hungry"
                };

                var WIKI = new Wiki();
                if ((WIKI = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    //ACT
                    var status = wiki_controller.Patch(WIKI.Url, wiki);

                    //ASSERT
                    Assert.Equal(StatusCodes.Status204NoContent.ToString(), status.ToString());
                }
            }
        }

        [Fact]
        void PatchWiki400Test()
        {
            //ARRANGE
            using (var context = inMEM("PatchWTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var wrepo = new WikiRepositoryStoring(context, factory);
                var prepo = new PageRepositoryStoring(context, factory);
                var wiki_controller = new WikiController(wrepo, prepo, w_logger);

                var WIKI = new Wiki();
                if ((WIKI = context.Wiki.Where(w => w.Url == "training-code").SingleOrDefault()) != null)
                {
                    //ACT
                    var status = wiki_controller.Patch(WIKI.Url, null);

                    //ASSERT
                    Assert.Equal(StatusCodes.Status400BadRequest.ToString(), status.ToString());
                }
            }
        }

        ViewzDbContext inMEM(string instance)
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
