using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    interface IMdToHtmlAndContentsFactory
    {
        internal IHtmlAndContents GetResult(string markDown);
    }
}
