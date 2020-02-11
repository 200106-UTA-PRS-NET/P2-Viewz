using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewzApi.Models
{
    public class Images
    {
        public int WikiId { get; set; }
        public string ImageName { get; set; }
        public byte[] Image { get; set; } 
        public virtual Wiki Wiki { get; set; }
    }
}
