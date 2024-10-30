using LikeTrackerAPI.Commons.DTOs;
using LikeTrackerAPI.Data;
using LikeTrackerAPI.Interfaces;

namespace LikeTrackerAPI.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly LikeTrackerContext _context;

        public ArticleService(LikeTrackerContext context)
        {
            _context = context;
        }

        public async Task<LikeResponseDto> GetLikesCountAsync(int articleId)
        {
            var response = new LikeResponseDto();
            var article = await _context.Articles.FindAsync(articleId);
            if (article == null)
            {
                throw new KeyNotFoundException("Article not found.");
            }

            return new LikeResponseDto
            {
                LikesCount = article.LikesCount
            };
        }

        public async Task<LikeResponseDto> LikeArticleAsync(int articleId)
        {
            var response = new LikeResponseDto();   
            var article = await _context.Articles.FindAsync(articleId);
            if (article == null)
            {
                throw new KeyNotFoundException("Article not found.");
            }

            article.LikesCount++;
            await _context.SaveChangesAsync();

            return new LikeResponseDto
            {
                LikesCount = article.LikesCount
            };
        }
    }
}
