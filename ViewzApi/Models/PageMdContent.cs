using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewzApi.Models
{
    public class PageMdContent
    {
        public long PageId { get; set; }
        public string MdContent { get; set; }
        public virtual Page Page { get; set; }
    }
}
