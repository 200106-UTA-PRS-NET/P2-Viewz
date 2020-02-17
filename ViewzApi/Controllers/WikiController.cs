using System;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViewzApi.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ViewzApi.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/Wiki/{WikiUrl}")]
    [ApiController]
    public class WikiController : ControllerBase
    {
        private readonly IWikiRepository _wikiRepository;
        private readonly IPageRepository _repository;
        private readonly ILogger _logger;

        //public WikiController(IWikiRepository wikiRepository) { }

        public WikiController(IWikiRepository wikiRepository, IPageRepository repository, ILogger<WikiController> logger)
        {
            _wikiRepository = wikiRepository;
            _repository = repository;
            _logger = logger;
        }

        //get popular wikis
        [HttpGet(Name = "GetPopular")]
        //[HttpGet]
        //api/wiki
        public IActionResult Get([FromQuery]uint count = 1, [FromQuery]bool description = false)
        {
            try
            {
                return Ok(_wikiRepository.GetPopularWikis(count, description));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return NotFound();
            }
        }

        //get one wiki
        [HttpGet("{WikiURL}", Name = "GetWiki")]
        public IActionResult Get([FromRoute]string WikiURL, bool html = true)
        {
            try
            {
                var repoWiki = (html) ? _wikiRepository.GetWikiWithHTML(WikiURL) : _wikiRepository.GetWikiWithMD(WikiURL);

                Wiki wiki = new Wiki()
                {
                    Url = repoWiki.Url,
                    PageName = repoWiki.PageName ?? WikiURL,
                    Description = (html) ? repoWiki.HtmlDescription : repoWiki.MdDescription,
                    PopularPages = (from repoPage in _repository.GetPopularPages(WikiURL, 3)
                                    select new Page()
                                    {
                                       // Content = null,
                                        Content = (html) ? repoPage.HtmlContent : repoPage.MdContent,
                                        Details = repoPage.Details,
                                        Contents = repoPage.Contents,
                                        //WikiUrl = WikiURL,
                                        Url = repoPage.Url,
                                        PageName = repoPage.PageName ?? repoPage.Url
                                    })
                };

                return Ok(wiki);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound();
            }
        }


        [HttpPost("{WikiURL}")]
        public IActionResult Post([FromRoute] string WikiUrl, [FromBody]Wiki wiki)
        {
            try
            {
                if (wiki.PageName != null)
                {
                    _wikiRepository.NewWiki(WikiUrl, wiki.PageName, wiki.Description);
                }
                else
                {
                    _wikiRepository.NewWiki(WikiUrl, wiki.Description);
                }

                return CreatedAtAction(actionName: nameof(Get), routeValues: new { WikiUrl }, value: null);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                //if request is duplicated 
                return Conflict("Post request must be unique");
            }
        }


        [HttpPatch]
        public IActionResult Patch([FromRoute] string WikiUrl, [FromBody]Wiki wiki)
        {
            try
            {
                if (wiki.PageName == null && wiki.Description == null)
                {
                    return BadRequest("wiki values cannot all be null");
                }

                if (wiki.PageName != null)
                {
                    _wikiRepository.SetName(WikiUrl, wiki.PageName);
                }

                if (wiki.Description != null)
                {
                    _wikiRepository.SetMD(WikiUrl, wiki.Description);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return NoContent();
        }
    }
}
