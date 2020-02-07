﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Page
    {
        public Page()
        {
            PageDetails = new HashSet<PageDetails>();
        }

        public int WikiId { get; set; }
        public long PageId { get; set; }
        public string Url { get; set; }
        public string PageName { get; set; }
        public string Content { get; set; }
        public string HtmlContent { get; set; }

        public virtual Wiki Wiki { get; set; }
        public virtual ICollection<PageDetails> PageDetails { get; set; }
    }
}
