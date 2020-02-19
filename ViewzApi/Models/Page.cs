using System.Collections.Generic;

namespace ViewzApi.Models
{
    public class Page
    {
        public string Url { get; set; }
        public string PageName { get; set; }  
        public string Content { get; set; }
        public IEnumerable<DataAccess.Storing.Contents> Contents { get; set; }
        public IEnumerable<DataAccess.Storing.PageDetails> Details { get; set; }
    }
}
