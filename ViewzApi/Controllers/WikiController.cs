using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using ViewzApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ViewzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WikiController : ControllerBase
    {
        private readonly IWikiRepository _wikiRepository;
        private readonly IPageRepository _repository;
        private readonly ILogger _logger;
        public WikiController(IWikiRepository wikiRepository, IPageRepository repository, ILogger<WikiController> logger)
        {
            _wikiRepository = wikiRepository;
            _repository = repository;
            _logger = logger;
        }

        //[HttpGet]
        ////api/wiki
        //public IEnumerable<Wiki> Get(bool html = true)
        //{
        //    return (from repoWiki in _wikiRepository.GetPopularWikis(5, false) 
        //            select new Wiki()
        //            {
        //                Url = repoWiki.Url,
        //                PageName = repoWiki.PageName,
        //                Description = (html) ? repoWiki.HtmlDescription : repoWiki.MdDescription
        //            }); 
        //}

        //GET: api/wiki/some-url 
        [HttpGet("{WikiURL}", Name = "GetPopularPages")]
        public IEnumerable<Page> Get([FromRoute]string WikiURL)
        {
            return (from repoPage in _repository.GetPopularPages(WikiURL, 5)
                    select new Page()
                    {
                        Content = null,
                        Details = repoPage.Details,
                        Contents = repoPage.Contents,
                        WikiUrl = WikiURL,
                        Url = repoPage.Url,
                        PageName = repoPage.PageName ?? repoPage.Url
                    });

        }


        //[HttpGet("{WikiURL}", Name = "GetPopularPages")]
        //public Wiki Get([FromRoute]string WikiURL)
        //{
        //    return (from repoWiki in _wikiRepository.GetWikiWithHTML(WikiURL)
        //            select new Wiki() {
        //                Description = repoWiki
        //            });  
        //}
        /*
         
        [HttpPost]
        public IActionResult Post([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody]Page page)
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
                
                return CreatedAtAction(actionName: nameof(Get), routeValues: new { WikiUrl, PageUrl }, value: null);
            }
            catch (Exception e)
            { 
                _logger.LogError(e.Message);
                //if request is duplicated 
                return Conflict("Post request must be unique");
            }
        }

        
        [HttpPatch]
        public IActionResult Patch([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody]Page page)
        {
            try
            {
                if (page.Content == null && page.PageName == null && page.Details == null)
                { 
                    return BadRequest("page values cannot all be null");
                }

                if (page.PageName != null)
                {
                    _repository.SetName(WikiUrl, PageUrl, page.PageName);
                }

                if (page.Content != null)
                {
                    _repository.SetMD(WikiUrl, PageUrl, page.Content);

                }

                if (page.Details != null)
                {
                    _repository.SetPageDetails(WikiUrl, PageUrl, page.Details);
                }
            }
            catch (Exception e) {
                _logger.LogError(e.Message); 
            }

            return NoContent();
        } 
        */
        //api/wiki/url-of-wiki
        //[HttpGet("{url}", Name = "GetWiki")]
        //public Wiki Get([FromRoute]string url, [FromQuery]bool Content = true)
        //{
        //    Wiki Wiki = new Wiki();

        //    if (Wikis.Exists(w => w.Url == url))
        //    {
        //        Wiki = Wikis.FirstOrDefault(w => w.Url == url);
        //    }

        //    return Wiki;
        //}

        //public WikiHtmlDescription Get() 
        //{ 
        //    return _repository.WikiHtmlDescription();
        //} 
    }
}
