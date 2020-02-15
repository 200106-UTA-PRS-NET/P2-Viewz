//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ViewzApi.Models;

namespace ViewzApi.Models
{
    public class Page
    {
        public string WikiUrl { get; set; }
        public string Url { get; set; }
        public string PageName { get; set; }  
        public string Content { get; set; }
        public IEnumerable<DataAccess.Storing.Contents> Contents { get; set; }
        public IEnumerable<DataAccess.Storing.PageDetails> Details { get; set; }
    }
}
