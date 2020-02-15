//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

namespace ViewzApi.Models
{
    public class PageDetails
    {
        public long PageId { get; set; }
        public string DetKey { get; set; }
        public string DetValue { get; set; }
        public virtual Page Page { get; set; }
    }
}
