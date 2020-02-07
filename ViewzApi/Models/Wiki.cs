using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewzApi.Models;

namespace ViewzApi
{
    public class Wiki
    {
        public Wiki()
        {
            Page = new HashSet<Page>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string PageName { get; set; }

        public string Description { get; set; }
        public virtual ICollection<Page> Page { get; set; }
    }
}
