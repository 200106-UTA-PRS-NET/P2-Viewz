using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewzApi.Models
{
    public class PageHtmlContent
    {
        public long PageId { get; set; }
        public string HtmlContent { get; set; }
        public virtual Page Page { get; set; }
    }
}
