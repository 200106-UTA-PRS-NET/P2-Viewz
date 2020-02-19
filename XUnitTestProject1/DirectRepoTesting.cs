using DataAccess.APIAccess;
using DataAccess.Exceptions;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class DirectRepoTesting
    {
        [Theory]
        [InlineData("training-code")]
        [InlineData("bad-wiki")]
        public async Task WikiGetHTMLTestAsync(string wikiURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var wrepo = new WikiRepositoryStoring(context, factory);
            var wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            if(wiki != null)
            {
                await wrepo.GetHTMLAsync(wikiURL);
            } else
            {
                await Assert.ThrowsAsync<WikiNotFoundException>(() => wrepo.GetHTMLAsync(wikiURL));
            }
        }

        [Theory]
        [InlineData("training-code")]
        [InlineData("bad-wiki")]
        public async Task WikiGetMDTestAsync(string wikiURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var wrepo = new WikiRepositoryStoring(context, factory);
            var wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            if (wiki != null)
            {
                await wrepo.GetMDAsync(wikiURL);
            }
            else
            {
                await Assert.ThrowsAsync<WikiNotFoundException>(() => wrepo.GetMDAsync(wikiURL));
            }
        }

        [Theory]
        [InlineData("training-code")]
        [InlineData("bad-wiki")]
        public async Task WikiGetWikiTestAsync(string wikiURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var wrepo = new WikiRepositoryStoring(context, factory);
            var wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            if (wiki != null)
            {
                await wrepo.GetWikiAsync(wikiURL);
            }
            else
            {
                await Assert.ThrowsAsync<WikiNotFoundException>(() => wrepo.GetWikiAsync(wikiURL));
            }
        }

        [Theory]
        [InlineData("training-code")]
        [InlineData("bad-wiki")]
        public async Task WikiGetWikiMDTestAsync(string wikiURL)
        {
            string dbString = Guid.NewGuid().ToString();
            //ARRANGE
            using var context = InMemoryDbContextFactory.GetViewzDbContext(dbString);
            IMdToHtmlAndContentsFactory factory = new MdToHtmlAndContentsFactory();
            var wrepo = new WikiRepositoryStoring(context, factory);
            var wiki = await context.Wiki.Where(w => w.Url == wikiURL).SingleOrDefaultAsync();
            if (wiki != null)
            {
                await wrepo.GetWikiWithMDAsync(wikiURL);
            }
            else
            {
                await Assert.ThrowsAsync<WikiNotFoundException>(() => wrepo.GetWikiWithMDAsync(wikiURL));
            }
        }
    }
}
