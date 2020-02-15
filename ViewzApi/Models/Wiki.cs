using System.Collections.Generic;

namespace ViewzApi.Models
{
    public class Wiki
    { 
        public string Url { get; set; }
        public string PageName { get; set; }
        public string Description { get; set; }

        public IEnumerable<DataAccess.Storing.Page> PopularPages { get; set; }
    }
}
