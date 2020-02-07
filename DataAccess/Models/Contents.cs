using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Contents
    {
        public int WikiId { get; set; }
        public long PageId { get; set; }
        // Name of the content
        public string Content { get; set; }
        // Id of the content in the form x.y.z
        public string Id { get; set; }

        public virtual Page Page { get; set; }
    }
}
