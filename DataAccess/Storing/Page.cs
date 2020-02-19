using System.Collections.Generic;

namespace DataAccess.Storing
{
    public class Page
    {
        public string WikiUrl { get; set; }
        public string Url { get; set; }
        public string PageName { get; set; }
        public string MdContent { get; set; }
        public string HtmlContent { get; set; }
        public IEnumerable<Contents> Contents {get; set;}
        public IEnumerable<PageDetails> Details { get; set; }
    }
}
