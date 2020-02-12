﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewzApi.Models;

namespace ViewzApi
{
    public class Page
    {
        public Page()
        {
            Contents = new HashSet<Contents>();
            PageDetails = new HashSet<PageDetails>();
        }

        //public int WikiId { get; set; }
        //public long PageId { get; set; }
        public string Url { get; set; }
        public string PageName { get; set; }
 
        public string Content { get; set; }
        //public virtual Wiki Wiki { get; set; }
        //public virtual PageHtmlContent PageHtmlContent { get; set; }
        //public virtual PageMdContent PageMdContent { get; set; }
        public virtual ICollection<Contents> Contents { get; set; }
        public virtual ICollection<PageDetails> PageDetails { get; set; }
    }
}