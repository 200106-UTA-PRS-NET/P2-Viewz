using DataAccess.Storing;
//using System;
using System.Collections.Generic;
//using System.Text;

namespace DataAccess.Interfaces
{
    public interface IWikiRepository
    {
        // Getters
        string GetMD(string wikiURL);
        string GetHTML(string wikiURL);
        Wiki GetWiki(string wikiURL);
        Wiki GetWikiWithMD(string wikiURL);
        Wiki GetWikiWithHTML(string wikiURL);
        IEnumerable<Wiki> GetPopularWikis(uint count, bool description = false);

        // Setters
        void SetMD(string wikiURL, string content);
        void SetName(string wikiURL, string newName);
        void NewWiki(string wikiURL, string pageName, string content);
        void NewWiki(string wikiURL, string content);
    }
}
