using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IMdToHtmlAndContentsFactory
    {
        public IHtmlAndContents GetResult(string markDown);
    }
}
