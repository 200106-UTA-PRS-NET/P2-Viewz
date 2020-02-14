using System;
using Xunit;
using DataAccess.APIAccess;
using DataAccess.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace XUnitTestProject1
{
    public class UnitTest1
    {

        [Fact]
        public async Task GetHtmlTest()
        {
            var factory = new MdToHtmlAndContentsFactory();
            string md = "# Header1 in\n## Header2 in\n### Header3 in\n#### Header4 out\n##### Header5 out\n###### Header6 out";
            string EXPECTED = "<h1>\n<a id=\"user-content-header1-in\"class=\"anchor\" href=\"#header1-in\"\naria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header1 in</h1>\n<h2>\n<a id=\"user-content-header2-in\" class=\"anchor\" href=\"#header2-in\"\naria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header2 in</h2>\n<h3>\n<a id=\"user-content-header3-in\" class=\"anchor\" href=\"#header3-in\"\naria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header3 in</h3>\n<h4>\n<a id=\"user-content-header4-out\" class=\"anchor\" href=\"#header4-out\"\naria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header4 out</h4>\n<h5>\n<a id=\"user-content-header5-out\" class=\"anchor\" href=\"#header5-out\"\naria-hidden=\"true\"><span aria-hidden=\"true\"class=\"octicon octicon-link\"></span></a>Header5 out</h5>\n<h6>\n<a id=\"user-content-header6-out\" class=\"anchor\" href=\"#header6-out\"\naria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header6 out</h6>";
            string ACTUAL = await factory.GetHtml(md);
            ACTUAL.Trim('\t');
            ACTUAL.Trim(' ');
            Assert.Equal(EXPECTED, ACTUAL);
        }

        /*
        [Fact]
        public void GetHtmlAndContentsTestNull()
        {
            var factory = new MdToHtmlAndContentsFactory();
            string md = null;
            IHtmlAndContents EXPECTED = null;
            IHtmlAndContents ACTUAL = factory.GetHtmlAndContents(md);
            Assert.Equal(EXPECTED, ACTUAL);
        }
        */
    }
}
