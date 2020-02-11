using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Contents
    {
        public long PageId { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }

        public virtual Page Page { get; set; }
    }
}
