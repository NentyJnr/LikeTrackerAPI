using LikeTrackerAPI.Commons.DTOs;
using LikeTrackerAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LikeTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [ProducesResponseType(typeof(LikeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(LikeResponseDto), StatusCodes.Status500InternalServerError)]
        [HttpGet("Get-like-counts")]
        public async Task<IActionResult> GetLikesCount([FromBody] int articleId)
        {
            try
            {
                var response = await _articleService.GetLikesCountAsync(articleId);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [ProducesResponseType(typeof(LikeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(LikeResponseDto), StatusCodes.Status500InternalServerError)]
        [HttpPost("{id}/like")]
        public async Task<IActionResult> LikeArticle([FromBody] int articleId)
        {
            try
            {
                var response = await _articleService.LikeArticleAsync(articleId);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
