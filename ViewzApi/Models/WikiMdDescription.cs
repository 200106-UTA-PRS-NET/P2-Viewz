using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewzApi.Models
{
    public class WikiMdDescription
    {
        public int WikiId { get; set; }
        public string MdDescription { get; set; }
        public virtual Wiki Wiki { get; set; }
    }
}
