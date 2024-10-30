using LikeTrackerAPI.Commons.DTOs;

namespace LikeTrackerAPI.Interfaces
{
    public interface IArticleService
    {
        Task<LikeResponseDto> GetLikesCountAsync(int articleId);
        Task<LikeResponseDto> LikeArticleAsync(int articleId);
    }
}
