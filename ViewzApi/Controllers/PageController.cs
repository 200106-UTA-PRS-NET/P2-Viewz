using System;
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
        public string Get([FromRoute] string WikiUrl, [FromRoute] string PageUrl,
                        bool details = true, bool html = true, bool content = true)
        {
            try
            {
                if (html)
                {
                    return _repository.GetHTML(WikiUrl, PageUrl);
                }

                else
                {
                    return _repository.GetMD(WikiUrl, PageUrl);
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }

        }
        


        [HttpPost]
        public void Post([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody] string content)
        {
            _repository.NewPage(WikiUrl, PageUrl, content);
            _repository.SetMD(WikiUrl, PageUrl, content);
        }


        [HttpPost]
        public void Post([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody] string PageName, [FromBody] string content)
        {
            _repository.NewPage(WikiUrl, PageUrl, PageName, content);
            _repository.SetMD(WikiUrl, PageUrl, content);
        }


        [HttpPut]
        public void Put()
        {
            
        }



    }
}
