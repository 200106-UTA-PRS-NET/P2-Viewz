using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IPageRepository
    {
        // Getters
        public string GetMD(string wikiURL, string pageURL);
        public string GetHTML(string wikiURL, string pageURL);
        // Setters
        public void SetMD(string wikiURL, string pageURL, string content);
        public void SetHTML(string wikiURL, string pageURL, string content);
    }
}
