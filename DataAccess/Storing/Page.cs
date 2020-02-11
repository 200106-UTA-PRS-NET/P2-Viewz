using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Storing
{
    class Page
    {
        public string WikiUrl { get; set; }
        public string Url { get; set; }
        public string PageName { get; set; }
        public string MdContent { get; set; }
        public string HtmlContent { get; set; }
    }
}
