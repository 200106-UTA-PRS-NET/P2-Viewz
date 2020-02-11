using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewzApi.Models
{
    public class WikiHtmlDescription
    {
        public int WikiId { get; set; }
        public string HtmlDescription { get; set; }
        public virtual Wiki Wiki { get; set; }
    }
}
