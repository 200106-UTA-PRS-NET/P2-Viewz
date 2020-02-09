using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    interface IApiConnectionFactory
    {
        internal IApiResult GetResult(string markDown);
    }
}
