using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class PageMdContent
    {
        public long PageId { get; set; }
        public string MdContent { get; set; }

        public virtual Page Page { get; set; }
    }
}
