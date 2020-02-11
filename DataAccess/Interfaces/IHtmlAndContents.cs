using DataAccess.Storing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IHtmlAndContents
    {
        public IEnumerable<Contents> Contents { get;}
        public string PageHTML { get; }
    }
}
