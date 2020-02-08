using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            //Arrange
            string content = "a woiejal lkwjeoije aiuoiwuya yowj <a href=\"Page1\">okay so this is a link</a> href=\"Page2\" ok so this is not... but it still works.";
            string to_replace = "href=\"";
            string replace_with = "href=\"Controller/Action/";
            string Expected = "a woiejal lkwjeoije aiuoiwuya yowj <a href=\"Controller/Action/Page1\">okay so this is a link</a> href=\"Controller/Action/Page2\" ok so this is not... but it still works.";

            //Act
            string Actual = Methods.Parsing.ReplaceInside(content, to_replace, replace_with);

            //Assert
            Assert.Equal(Expected, Actual);
        }
    }
}
