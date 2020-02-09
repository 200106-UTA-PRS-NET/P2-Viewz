using DataAccess.Storing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    interface IApiResult
    {
        IEnumerable<Contents> Contents { get; }
        string PageHTML { get; }
    }
}
