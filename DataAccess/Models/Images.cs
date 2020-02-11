using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Images
    {
        public int WikiId { get; set; }
        public string ImageName { get; set; }
        public byte[] Image { get; set; }

        public virtual Wiki Wiki { get; set; }
    }
}
