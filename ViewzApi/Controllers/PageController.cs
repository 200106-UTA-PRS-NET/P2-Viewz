using System;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViewzApi.Models;
using DataAccess.Exceptions;
using System.Threading.Tasks;

namespace ViewzApi.Controllers
{
    public enum PageContent
    {
        Md,
        Html,
        NoContent
    }
    [Route("api/Wiki/{WikiUrl}/{PageUrl}")]
    [ApiController]
    public class PageController : ControllerBase
    { 
        private readonly IPageRepository _repository;
        private readonly ILogger _logger;
      

        public PageController(IPageRepository repository, ILogger<PageController> logger)
        { 
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromRoute] string WikiUrl, [FromRoute] string PageUrl,
                      PageContent html=PageContent.NoContent, bool details = true, bool content=true)
        {
            try
            {
                DataAccess.Storing.Page repoPage;
                Page page = new Page();

                if (html == PageContent.Html)
                {
                    repoPage = await _repository.GetPageWithHTMLAsync(WikiUrl, PageUrl);
                    page.Content = repoPage.HtmlContent; 
                }
                else if (html==PageContent.Md)
                {
                    repoPage = await _repository.GetPageWithMDAsync(WikiUrl, PageUrl);
                    page.Content = repoPage.MdContent;
                }
                else {
                    repoPage = await _repository.GetPageAsync(WikiUrl, PageUrl);
                    page.Content = null;
                }
                 
                page.Details = (details) ? repoPage.Details : null;
                page.Contents = (content) ? repoPage.Contents : null; 
                page.Url = repoPage.Url ?? PageUrl;
                page.PageName = repoPage.PageName ?? PageUrl;
            
                return Ok(page);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound("page could not be found");
            }
        }
         
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody]Page page)
        {
            try
            {
                if (page.PageName != null)
                {
                    await _repository.NewPageAsync(WikiUrl, PageUrl, page.PageName, page.Content);
                   
                }
                else
                {
                    await _repository.NewPageAsync(WikiUrl, PageUrl, page.Content);
                }

                await _repository.SetPageDetailsAsync(WikiUrl, PageUrl,page.Details);

                return CreatedAtAction(actionName: nameof(GetAsync), routeValues: new { WikiUrl, PageUrl }, value: null);
            }
            catch(WikiNotFoundException e)
            {
                _logger.LogError(e.Message);
                return NotFound($"{WikiUrl} not found");
            }
            catch (PageExistsException e)
            { 
                _logger.LogError(e.Message);
                //if request is duplicated 
                return Conflict("Post request must be unique");
            }
        }

        
        [HttpPatch]
        public async Task<IActionResult> PatchAsync([FromRoute] string WikiUrl, [FromRoute] string PageUrl, [FromBody]Page page)
        {
            try
            {
                if (page.Content == null && page.PageName == null && page.Details == null)
                { 
                    return BadRequest("page values cannot all be null");
                }

                if (page.PageName != null)
                {
                    await _repository.SetNameAsync(WikiUrl, PageUrl, page.PageName);
                }

                if (page.Content != null)
                {
                    await _repository.SetMDAsync(WikiUrl, PageUrl, page.Content);

                }

                if (page.Details != null)
                {
                    await _repository.SetPageDetailsAsync(WikiUrl, PageUrl, page.Details);
                }
            }
            catch (WikiNotFoundException e) {
                _logger.LogError(e.Message);
                return NotFound($"{WikiUrl} not found");
            }
            catch (PageNotFoundException e)
            {
                _logger.LogError(e.Message);
                return NotFound($"{WikiUrl}/{PageUrl} not found");
            }

            return NoContent();
        } 
    }
}
