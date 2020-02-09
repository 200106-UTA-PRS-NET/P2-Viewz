using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class WikiHtmlDescription
    {
        public int WikiId { get; set; }
        public string HtmlDescription { get; set; }

        public virtual Wiki Wiki { get; set; }
    }
}
