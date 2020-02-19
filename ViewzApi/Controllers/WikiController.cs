using System;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViewzApi.Models; 
using System.Linq;
using DataAccess.Exceptions;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAsync([FromQuery]uint count = 1, [FromQuery]bool description = false)
        {
            return Ok(await _wikiRepository.GetPopularWikisAsync(count, description));
        }

        //get one wiki
        [HttpGet("{WikiURL}", Name = "GetWiki")]
        public async Task<IActionResult> GetAsync([FromRoute]string WikiURL, bool html = true)
        {
            try
            {
                var repoWiki = await ((html) ? _wikiRepository.GetWikiWithHTMLAsync(WikiURL) : _wikiRepository.GetWikiWithMDAsync(WikiURL));

                Wiki wiki = new Wiki()
                {
                    Url = repoWiki.Url,
                    PageName = repoWiki.PageName ?? WikiURL,
                    Description = (html) ? repoWiki.HtmlDescription : repoWiki.MdDescription,
                    PopularPages = (from repoPage in await _repository.GetPopularPagesAsync(WikiURL, 5)
                                    select new Page()
                                    { 
                                        Content = (html) ? repoPage.HtmlContent : repoPage.MdContent,
                                        Details = repoPage.Details,
                                        Contents = repoPage.Contents, 
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
        public async Task<IActionResult> PostAsync([FromRoute] string WikiUrl, [FromBody]Wiki wiki)
        {
            try
            {
                if (wiki.PageName != null)
                {
                    await _wikiRepository.NewWikiAsync(WikiUrl, wiki.PageName, wiki.Description);
                }
                else
                {
                    await _wikiRepository.NewWikiAsync(WikiUrl, wiki.Description);
                }

                return CreatedAtAction(actionName: nameof(GetAsync), routeValues: new { WikiUrl }, value: null);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                //if request is duplicated 
                return Conflict("Post request must be unique");
            }
        }


        [HttpPatch("{WikiURL}")]
        public async Task<IActionResult> PatchAsync([FromRoute] string WikiUrl, [FromBody]Wiki wiki)
        {
            try
            {
                if (wiki.PageName == null && wiki.Description == null)
                {
                    return BadRequest("wiki values cannot all be null");
                }

                if (wiki.PageName != null)
                {
                    await _wikiRepository.SetNameAsync(WikiUrl, wiki.PageName);
                }

                if (wiki.Description != null)
                {
                    await _wikiRepository.SetMDAsync(WikiUrl, wiki.Description);
                }
            }
            catch (WikiNotFound e)
            {
                _logger.LogError(e.Message);
                return NotFound();
            }

            return NoContent();
        }
    }
}
