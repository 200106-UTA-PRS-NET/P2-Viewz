using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class WikiMdDescription
    {
        public int WikiId { get; set; }
        public string MdDescription { get; set; }

        public virtual Wiki Wiki { get; set; }
    }
}
