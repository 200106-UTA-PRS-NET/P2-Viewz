using DataAccess.Storing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IWikiRepository
    {
        // Getters
        IEnumerable<Wiki> GetPopularWikis(uint count);
        string GetMD(string wikiURL);
        string GetHTML(string wikiURL);
        string GetName(string wikiURL);

        // Setters
        void SetMD(string wikiURL, string content);
        void NewWiki(string wikiURL, string pageName, string content);
        void NewWiki(string wikiURL, string content);
    }
}
