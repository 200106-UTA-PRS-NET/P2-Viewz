﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewzApi.Models
{
    public class Page
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

        public virtual Wiki Wiki { get; set; }
        public virtual ICollection<PageDetails> PageDetails { get; set; }
    }
}