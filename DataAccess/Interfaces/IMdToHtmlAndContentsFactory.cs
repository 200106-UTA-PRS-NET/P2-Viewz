using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IMdToHtmlAndContentsFactory
    {
        public Task<IHtmlAndContents> GetHtmlAndContents(string markDown);
        public Task<string> GetHtml(string markDown);
    }
}
