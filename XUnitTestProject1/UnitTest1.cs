//using System;
using DataAccess.Storing;
using Xunit;
//using DataAccess.APIAccess;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("page-name")]
        public void SimpleTest(string name)
        {
            Page page = new Page() { 
                PageName = name
            };
            Assert.Equal(name, page.PageName);
        }
        [Theory]
        [InlineData("page-name")]
        public void SimpleFailingTest(string name)
        {
            Page page = new Page()
            {
                PageName = name
            };
            Assert.NotEqual(name, page.PageName);
        }
    }
}
