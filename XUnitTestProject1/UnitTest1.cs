using System;
using Xunit;
using DataAccess.APIAccess;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        /*
        [Fact]
        public void Test1()
        {
            //Arrange
            string content = "a woiejal lkwjeoije aiuoiwuya yowj <a href=\"Page1\">okay so this is a link</a> href=\"Page2\" This should remain the same";
            string to_replace = "href=\"";
            string replace_with = "href=\"Controller/Action/";
            string Expected = "a woiejal lkwjeoije aiuoiwuya yowj <a href=\"Controller/Action/Page1\">okay so this is a link</a> href=\"Page2\" This should remain the same";

            //Act
            string Actual = Methods.Parsing.ReplaceInside(content, to_replace, replace_with);

            //Assert
            Assert.Equal(Expected, Actual);
        }
        [Fact]
        public void DontReplaceLinks()
        {
            string content = "this is an <a href=\"https://www.google.com/\">Test</a> anchor to google.com";
            string to_replace = "href=\"";
            string replace_with = "href=\"Controller/Action/";
            string expected = "this is an <a href=\"https://www.google.com/\">Test</a> anchor to google.com";

            string actual = Methods.Parsing.ReplaceInside(content, to_replace, replace_with);

            Assert.Equal(expected, actual);
        }
        */
        /*
        [Fact]
        public void TestConsumptionAPI()
        {
            var factory = new MdToHtmlAndContentsFactory();
            string md = "# training-code\nThis repository will be used for sharing code, projects and notes\n\n## Environment Setup\n* [github](https://github.com)\n  *Create an account\n  * We will use this for version control, class examples, and submission of your assignments\n*[git for windows + git bash] (https://git-scm.com/downloads) \n     * installs linux-like bash environment(terminal)\n     * also installs git, for version control\n* [Slack] (https://slack.com)\n  * www.slack.com\n  * Create a slack account or join using the [magic link] (https://join.slack.com/t/revaturepro/shared_invite/enQtODcyNzMxNTAyOTAwLTI2ODI4ZTVhY2E4YTgwMjYzZDczNTkyNDVhZTJkZjExNjlkMGNlNTE3MDY3NzBiZjk5OGQxOTczZDE1MWM5Mzg).\n  * We will use this for communications between the group outside of work hours. \n\n### tools:\n  * [visual studio](https://visualstudio.microsoft.com/downloads/)\n     * atleast.net core workload required for week 1\n  * [visual studio code](https://code.visualstudio.com/download)\n  * [.net core sdk](https://dotnet.microsoft.com/download)\n     * lets us compile c# code.\n     * included with visual studio workload\n     * gives us \"dotnet\" command\n\n### [gitignore](https://github.com/dotnet/core/blob/master/.gitignore) \n\n### Useful Links\n*[Git Cheat Sheet](https://www.git-tower.com/blog/git-cheat-sheet)\n* [Git Basics](https://youtu.be/0fKg7e37bQE)\n* [Git Team Basics](https://youtu.be/oFYyTZwMyAg)\n\n*The most common laptops are Windows PCs.Where MacOS and Linux systems can use package managers, Windows prefers its own GUI wizards.*\n\n\n* [Hacker Rank](https://www.hackerrank.com/)\n  * Good source of practice. Use it often for practice.Of course, if you still have assigned work to do, that work takes precedence.\n* [learn about md files](https://guides.github.com/features/mastering-markdown/)\n  * it's always good to read and manage markdowns.\n  * Also[markdown cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet#headers)\\n";

            //ACTUAL and EXPECTED
            string EXPECTED = "<h1>\n< a id = \"user-content-training-code\" class=\"anchor\" href=\"#training-code\"\naria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>training-code</h1>\n<p>This repository will be used for sharing code, projects and notes</p>\n<h2>\n<a id = \"user-content-environment-setup\" class=\"anchor\" href=\"#environment-setup\"\naria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Environment Setup</h2>\n<ul>\n<li>\n<a href = \"https://github.com\" > github </ a >\n< ul >\n< li > Create an account</li>\n<li>We will use this for version control, class examples, and submission of your assignments</li>\n</ul>\n</li>\n<li>\n<a href = \"https://git-scm.com/downloads\" rel=\"nofollow\">git for windows + git bash</a>\n<ul>\n<li>installs linux-like bash environment (terminal)</li>\n<li>also installs git, for version control</li>\n</ul>\n</li>\n<li>\n<a href = \"https://slack.com\" rel= \"nofollow\" > Slack </ a >\n  \n  < ul >\n  \n  < li >< a href= \"http://www.slack.com\" rel= \"nofollow\" > www.slack.com </ a ></ li >\n  \n  < li > Create a slack account or join using the <a\nhref=\"https://join.slack.com/t/revaturepro/shared_invite/enQtODcyNzMxNTAyOTAwLTI2ODI4ZTVhY2E4YTgwMjYzZDczNTkyNDVhZTJkZjExNjlkMGNlNTE3MDY3NzBiZjk5OGQxOTczZDE1MWM5Mzg\"\nrel=\"nofollow\">magic link</a>.</li>\n<li>We will use this for communications between the group outside of work hours.</li>\n</ul>\n</li>\n</ul>\n<h3>\n<a id = \"user-content-tools\" class=\"anchor\" href=\"#tools\"\naria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>tools:</h3>\n<ul>\n<li>\n<a href = \"https://visualstudio.microsoft.com/downloads/\" rel=\"nofollow\">visual studio</a>\n  <ul>\n<li>atleast.net core workload required for week 1</li>\n</ul>\n</li>\n<li><a href = \"https://code.visualstudio.com/download\" rel= \"nofollow\" > visual studio code</a></li>\n<li>\n<a href = \"https://dotnet.microsoft.com/download\" rel= \"nofollow\" >.net core sdk</a>\n<ul>\n<li>lets us compile c# code.</li>\n<li>included with visual studio workload</li>\n  <li>gives us \"dotnet\" command</li>\n  </ul>\n</li>\n</ul>\n<h3>\n<a id = \"user-content-gitignore\" class=\"anchor\" href=\"#gitignore\"\naria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a><a\nhref = \"https://github.com/dotnet/core/blob/master/.gitignore\" > gitignore </ a >\n</ h3 >\n< h3 >\n< a id=\"user-content-useful-links\" class=\"anchor\" href=\"#useful-links\"\naria-hidden=\"true\"><span aria-hidden=\"true\" class=\"octicon octicon-link\"></span></a>Useful Links</h3>\n<ul>\n<li><a href = \"https://www.git-tower.com/blog/git-cheat-sheet\" rel= \"nofollow\" > Git Cheat Sheet</a></li>\n<li><a href = \"https://youtu.be/0fKg7e37bQE\" rel= \"nofollow\" > Git Basics</a></li>\n<li><a href = \"https://youtu.be/oFYyTZwMyAg\" rel= \"nofollow\" > Git Team Basics</a></li>\n</ul>\n<p><em>The most common laptops are Windows PCs.Where MacOS and Linux systems can use package managers, Windows prefers its own GUI wizards.</em>\n</p>\n<ul>\n<li>\n<a href = \"https://www.hackerrank.com/\" rel= \"nofollow\" > Hacker Rank</a>\n<ul>\n<li>Good source of practice. Use it often for practice.Of course, if you still have assigned work to do,\nthat work takes precedence.</li>\n</ul>\n</li>\n<li>\n<a href = \"https://guides.github.com/features/mastering-markdown/\" > learn about md files</a>\n<ul>\n<li>it's always good to read and manage markdowns.</li>\n<li>Also<a href=\"https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet#headers\"> markdown\ncheatsheet</a>\n</li>\n</ul>\n</li>\n</ul>";
            string ACTUAL = factory.GetResult(md).PageHTML;
            EXPECTED.Replace(" ", String.Empty);
            ACTUAL.Replace(" ", String.Empty);

            Assert.Equal(EXPECTED, ACTUAL);
		}
        */
    }
}
