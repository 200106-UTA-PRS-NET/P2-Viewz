//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IMdToHtmlAndContentsFactory
    {
        public Task<IHtmlAndContents> GetHtmlAndContents(string markDown);
        public Task<string> GetHtml(string markDown);
    }
}
