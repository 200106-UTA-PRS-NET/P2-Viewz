using DataAccess.Storing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IWikiRepository
    {
        // Getters
        Task<string> GetMDAsync(string wikiURL);
        Task<string> GetHTMLAsync(string wikiURL);
        Task<Wiki> GetWikiAsync(string wikiURL);
        Task<Wiki> GetWikiWithMDAsync(string wikiURL);
        Task<Wiki> GetWikiWithHTMLAsync(string wikiURL);
        Task<IEnumerable<Wiki>> GetPopularWikisAsync(uint count, bool description = false);
        
        // Setters
        Task SetMDAsync(string wikiURL, string content);
        Task SetNameAsync(string wikiURL, string newName);
        Task NewWikiAsync(string wikiURL, string pageName, string content);
        Task NewWikiAsync(string wikiURL, string content);
    }
}
