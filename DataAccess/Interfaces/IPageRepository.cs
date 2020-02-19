using DataAccess.Storing;
//using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using System.Text;

namespace DataAccess.Interfaces
{
    public interface IPageRepository
    {
        // Getters
        public Task<string> GetMDAsync(string wikiURL, string pageURL);
        public Task<string> GetHTMLAsync(string wikiURL, string pageURL);
        public Task<Page> GetPageAsync(string wikiURL, string pageURL);
        public Task<Page> GetPageWithMDAsync(string wikiURL, string pageURL);
        public Task<Page> GetPageWithHTMLAsync(string wikiURL, string pageURL);
        public Task<IEnumerable<Page>> GetPopularPagesAsync(string wikiURL, uint count);

      
        // Setters
        public Task SetMDAsync(string wikiURL, string pageURL, string content);
        public Task SetNameAsync(string wikiURL, string pageURL, string newName);
               
        public Task SetPageDetailsAsync(string wikiURL, string pageURL, IEnumerable<PageDetails> details);

        public Task NewPageAsync(string wikiURL, string pageURL, string content);
        public Task NewPageAsync(string wikiURL, string pageURL, string pageName, string content);

        
    }
}
