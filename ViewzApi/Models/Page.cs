using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewzApi.Models
{
    public class Page
    {
        public int Id { get; set; }

        public int WikiId { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }
}
