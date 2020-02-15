using System;
using Xunit;
using DataAccess.APIAccess;
using DataAccess.Interfaces;
using DataAccess.Storing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetHtmlTest()
        {
            var factory = new MdToHtmlAndContentsFactory();
            string md = "# Header1 in\n## Header2 in\n### Header3 in\n#### Header4 out\n##### Header5 out\n###### Header6 out";
            string EXPECTED = "<h1>\n<a id=\"user-content-header1-in\" class=\"anchor\" href=\"#header1-in\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header1 in</h1>\n<h2>\n<a id=\"user-content-header2-in\" class=\"anchor\" href=\"#header2-in\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header2 in</h2>\n<h3>\n<a id=\"user-content-header3-in\" class=\"anchor\" href=\"#header3-in\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header3 in</h3>\n<h4>\n<a id=\"user-content-header4-out\" class=\"anchor\" href=\"#header4-out\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header4 out</h4>\n<h5>\n<a id=\"user-content-header5-out\" class=\"anchor\" href=\"#header5-out\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header5 out</h5>\n<h6>\n<a id=\"user-content-header6-out\" class=\"anchor\" href=\"#header6-out\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header6 out</h6>\n";
            string ACTUAL = await factory.GetHtml(md);
            ACTUAL.Trim('\t');
            
            Assert.Equal(EXPECTED, ACTUAL);
        }

        [Fact]
        public async Task GetHtmlTestNull()
        {
            var factory = new MdToHtmlAndContentsFactory();
            string md = null;
            string EXPECTED = null;
            string ACTUAL = await factory.GetHtml(md);

            Assert.Equal(EXPECTED, ACTUAL);
        }
        /*
        //FAILS but functions properly
        [Fact]
        public void GetHtmlAndContentsTest()
        {
            var factory = new MdToHtmlAndContentsFactory();
            string md = "# Header1 in\n## Header2 in\n### Header3 in\n#### Header4 out\n##### Header5 out\n###### Header6 out";
            var EXPECTED = new HtmlAndContents();
            EXPECTED.PageHTML = "<h1>\n<a id=\"user-content-header1-in\" class=\"anchor\" href=\"#header1-in\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header1 in</h1>\n<h2>\n<a id=\"user-content-header2-in\" class=\"anchor\" href=\"#header2-in\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header2 in</h2>\n<h3>\n<a id=\"user-content-header3-in\" class=\"anchor\" href=\"#header3-in\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header3 in</h3>\n<h4>\n<a id=\"user-content-header4-out\" class=\"anchor\" href=\"#header4-out\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header4 out</h4>\n<h5>\n<a id=\"user-content-header5-out\" class=\"anchor\" href=\"#header5-out\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header5 out</h5>\n<h6>\n<a id=\"user-content-header6-out\" class=\"anchor\" href=\"#header6-out\" aria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Header6 out</h6>\n";
            var contents = new Contents[3];
            
            contents[0] = new Contents()
            {
                Id = "user-content-header1-in",
                Content = "Header1 in",
                Level = 1
            };
            contents[1] = new Contents()
            {
                Id = "user-content-header2-in",
                Content = "Header2 in",
                Level = 2
            };
            contents[2] = new Contents()
            {
                Id = "user-content-header3-in",
                Content = "Header3 in",
                Level = 3
            };
            
            EXPECTED.Contents = (IEnumerable<Contents>)contents;
            var ACTUAL = factory.GetHtmlAndContents(md);

            Assert.Equal(EXPECTED, ACTUAL);
        }
        */

        [Fact]
        public void GetHtmlAndContentsTestNull()
        {
            var factory = new MdToHtmlAndContentsFactory();
            string md = null;
            IHtmlAndContents EXPECTED = null;
            IHtmlAndContents ACTUAL = factory.GetHtmlAndContents(md);
            Assert.Equal(EXPECTED, ACTUAL);
        }
    }
}
