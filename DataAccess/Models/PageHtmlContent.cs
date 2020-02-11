using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class PageHtmlContent
    {
        public long PageId { get; set; }
        public string HtmlContent { get; set; }

        public virtual Page Page { get; set; }
    }
}
