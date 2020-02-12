using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewzApi.Models;

namespace ViewzApi.Controllers
{
    [Route("api/Wiki/{WikiUrl}/{PageUrl}")]
    [ApiController]
    public class PageController : ControllerBase
    { 
        private readonly IPageRepository _repository;

        public PageController(IPageRepository repository)
        { 
            _repository = repository; 
        }

        //url from db
        //api/wiki/training-code/readme/?html=false
        public IActionResult Get([FromRoute] string WikiUrl, [FromRoute] string PageUrl,
                        bool details = true, bool html = true, bool content = true)
        {
            try
            {
                //sets content in page based on Html bool, if true sets html
                //else gives back Md
                DataAccess.Storing.Page page;
                PageDetails pageDetails;
                Contents contents;
                /*
                Page page = new Page();
                if(details)
                {
                    //get pageDetails, set it to pageDetails variable
                    //page.PageDetails.Add(pageDetails);
                    
                } 
                if(content)
                {
                    //get Contents, set it to contents variable
                    //page.Contents.Add(contents);
                }
                */
                if (html)
                {

                    //page = new Page() { Content = _repository.GetHTML(WikiUrl, PageUrl)  };
                    page =  _repository.GetPageWithHTML(WikiUrl, PageUrl) ;

                }
                else
                {
                    //page = new Page() { Content = _repository.GetMD(WikiUrl, PageUrl) };
                    page = _repository.GetPageWithMD(WikiUrl, PageUrl);
                }

                return Ok(page);
            }
            catch (Exception e)
            {
                base.Content($"{e.ToString()}", "text/html");
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult Post([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody]Page page)
        {
            try
            {
                if (page.PageName != null)
                {
                    _repository.NewPage(WikiUrl, PageUrl, page.PageName, page.MdContent);
                }
                else
                {
                    _repository.NewPage(WikiUrl, PageUrl, page.MdContent);
                }

                /*
                    _repository.SetPageDetails 
                */

                return CreatedAtAction(actionName: nameof(Get), routeValues: new { WikiUrl, PageUrl }, value: null);
            }
            catch (Exception e)
            {
                base.Content($"{e.ToString()}", "text/html");
                return BadRequest();
            }
        }


        [HttpPut]
        public IActionResult Put([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody]Page page)
        {

            //if (page.Content == null && page.PageName == null)
            //{
            //    return BadRequest();
            //}
            ////if (page.PageName != null)
            ////{
            ////    //_repository.SetMD(WikiUrl, PageUrl, page.PageName, page.Content);
            ////}

            //if (page.Content != null)
            //{
            //    _repository.SetMD(WikiUrl, PageUrl, page.Content);
               
            //}
            
            return NoContent();
        }



    }
}
