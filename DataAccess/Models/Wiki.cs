using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Wiki
    {
        public Wiki()
        {
            Images = new HashSet<Images>();
            Page = new HashSet<Page>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string PageName { get; set; }
        public long HitCount { get; set; }

        public virtual WikiHtmlDescription WikiHtmlDescription { get; set; }
        public virtual WikiMdDescription WikiMdDescription { get; set; }
        public virtual ICollection<Images> Images { get; set; }
        public virtual ICollection<Page> Page { get; set; }
    }
}
