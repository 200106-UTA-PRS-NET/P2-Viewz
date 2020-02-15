//using System;
using System.Collections.Generic;
//using System.Text;
using System.Linq;

namespace DataAccess.Storing
{
    static class Mapper
    {
        internal static Storing.Contents Map(Models.Contents contents)
        {
            if (contents == null)
            {
                return null;
            }
            return new Storing.Contents()
            {
                Id = contents.Id,
                Content = contents.Content,
                Level = contents.Level
            };
        }
        internal static Storing.PageDetails Map(Models.PageDetails details)
        {
            if (details == null)
            {
                return null;
            }
            return new Storing.PageDetails()
            {
                DetKey = details.DetKey,
                DetValue = details.DetValue
            };
        }
        internal static Storing.Page Map(Models.Page page)
        {
            if (page == null)
            {
                return null;
            }
            return new Page()
            {
                PageName = page.PageName,
                Url = page.Url,
                HtmlContent = page.PageHtmlContent?.HtmlContent,
                MdContent = page.PageMdContent?.MdContent,
                Contents = (from content in page.Contents
                            orderby content.Order
                            select Map(content)).Skip(1),           //SMELL#52: removed ToList() from Map(content)).ToList().Skip(1)
                Details = (from detail in page.PageDetails
                           orderby detail.Order
                           select Map(detail)).ToList()
            };
        }

        internal static ICollection<Models.Contents> Map(IEnumerable<Contents> contents)
        {
            List<Models.Contents> retList = new List<Models.Contents>()
            {
                new Models.Contents()
                {
                    Order = 0,
                    Level = 0,
                    Id = "bad",
                    Content = "link"
                }
            };
            int i = 0;
            return retList.Concat((from content in contents
                    select new Models.Contents() {
                        Content = content.Content,
                        Level = content.Level,
                        Id = content.Id,
                        Order = ++i
                    }).ToList()).ToList();
        }

        internal static Wiki Map(Models.Wiki wiki)
        {
            return new Storing.Wiki()
            {
                PageName = wiki.PageName,
                Url = wiki.Url,
                HtmlDescription = wiki.WikiHtmlDescription?.HtmlDescription,
                MdDescription = wiki.WikiMdDescription?.MdDescription
            };
        }

        internal static ICollection<Models.PageDetails> Map(IEnumerable<PageDetails> details)
        {
            int i = 0;
            return (from detail in details
                    select new Models.PageDetails()
                    {
                        DetKey = detail.DetKey,
                        DetValue = detail.DetValue,
                        Order = ++i
                    }).ToList();
        }
    }
}
