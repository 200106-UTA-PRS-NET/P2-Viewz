using System;
using System.Collections.Generic;
using System.Text;
using ViewzApi.Controllers;
using DataAccess.Models;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using DataAccess.APIAccess;

/*
         // Getters
        public string GetMD(string wikiURL, string pageURL);
        public string GetHTML(string wikiURL, string pageURL);
        public Page GetPage(string wikiURL, string pageURL);
        public Page GetPageWithMD(string wikiURL, string pageURL);
        public Page GetPageWithHTML(string wikiURL, string pageURL);
        public IEnumerable<Page> GetPopularPages(string wikiURL, uint count);

      
        // Setters
        public void SetMD(string wikiURL, string pageURL, string content);
        public void SetName(string wikiURL, string pageURL, string newName);

        public void SetPageDetails(string wikiURL, string pageURL, IEnumerable<PageDetails> details);

        public void NewPage(string wikiURL, string pageURL, string content);
        public void NewPage(string wikiURL, string pageURL, string pageName, string content);
*/
namespace Tests
{
    class APIServiceTesting
    {
        void GetMDTest()
        {
            using(var context = returnContext("GetMDTest"))
            {
                IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
                var repo = new PageRepositoryStoring(context, factory);

                //make a default database that has ids that link properly and ... that link properly but not relational necessarily
                //then test with repo methods
                
            }
        }

        ViewzDbContext returnContext(string instance)
        {
            var options = new DbContextOptionsBuilder<ViewzDbContext>().UseInMemoryDatabase(databaseName: instance).Options;
            var context = new ViewzDbContext(options);

            var wiki = new Wiki()
            {
                Id = 1,
                Url = "training-code",
                PageName = "Training Code",
                HitCount = 0,
                Page = new List<Page>()
            };
            var page = new Page()
            {
                WikiId = 1,
                PageId = 1,
                Url = "readme",
                PageName = "Read Me",
                HitCount = 0,
                Wiki = wiki,
            };
            wiki.Page.Add(page);
            var contents = new Contents()
            {

            };
            
            //context.Add(); each
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
