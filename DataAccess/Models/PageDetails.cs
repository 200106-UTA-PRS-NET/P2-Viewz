using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class PageDetails
    {
        public long PageId { get; set; }
        public string DetKey { get; set; }
        public string DetValue { get; set; }

        public virtual Page Page { get; set; }
    }
}
