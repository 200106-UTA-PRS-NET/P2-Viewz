using System.Collections.Generic;
using DataAccess.Interfaces;
using DataAccess.Storing;

namespace DataAccess.APIAccess
{
    public class HtmlAndContents : IHtmlAndContents
    {
        public IEnumerable<Contents> Contents { get; set; }
        public string PageHTML { get; set; }
    }
}
