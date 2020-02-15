//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.APIAccess;
//using DataAccess.Interfaces;
using DataAccess.Storing;

namespace ViewzApp.Controllers
{
    public class TestController : ControllerBase
    {
        public IEnumerable<Contents> Index()
        {
            var factory = new MdToHtmlAndContentsFactory();

            string md = "# Header1 in\n## Header2 in\n### Header3 in\n#### Header4 out";

            return factory.GetHtmlAndContents(md).Contents;
        }
    }
}