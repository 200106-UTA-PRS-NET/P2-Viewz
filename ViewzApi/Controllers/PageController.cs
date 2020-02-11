﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 

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
        public Page Get([FromRoute] string WikiUrl, [FromRoute] string PageUrl,
                        bool details = true, bool html = true, bool content = true)
        {
            try
            {
                //sets content in page based on Html bool, if true return html
                //else gives back Md
                if (html)
                {
                    
                    return new Page() { Content = _repository.GetHTML(WikiUrl, PageUrl)  };
                }
                else
                {
                    return new Page() { Content = _repository.GetMD(WikiUrl, PageUrl) };
                } 
            }
            catch (Exception e)
            {
                base.Content($"{e.ToString()}", "text/html");
                return new Page();
            }

        }
        
        [HttpPost]
        public void Post([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody]Page page)
        {
            try
            {
                if (page.PageName != null)
                {
                    _repository.NewPage(WikiUrl, PageUrl, page.PageName, page.Content);
                }
                else
                {
                    _repository.NewPage(WikiUrl, PageUrl, page.Content);
                }

                _repository.SetMD(WikiUrl, PageUrl, page.Content);
            }
            catch (Exception e) {
                base.Content($"{e.ToString()}", "text/html");
            }
        }
    

        //[HttpPut]
        //public void Put()
        //{
            
        //}



    }
}
