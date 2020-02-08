using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Wiki
    {
        public Wiki()
        {
            Page = new HashSet<Page>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string PageName { get; set; }
        public string DescriptionMd { get; set; }
        public string DescriptionHtml { get; set; }

        public virtual ICollection<Page> Page { get; set; }
    }
}
