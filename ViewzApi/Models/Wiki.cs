using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewzApi.Models;

namespace ViewzApi
{
    public class Wiki
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public List<Page> Pages { get; set; }
    }
}
