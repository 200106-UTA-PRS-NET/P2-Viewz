using DataAccess.Storing;
//using System;
using System.Collections.Generic;
//using System.Text;

namespace DataAccess.Interfaces
{
    public interface IPageRepository
    {
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

        
    }
}
