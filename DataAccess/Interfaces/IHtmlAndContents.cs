using DataAccess.Storing;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IHtmlAndContents
    {
        public IEnumerable<Contents> Contents { get;}
        public string PageHTML { get; }
    }
}
