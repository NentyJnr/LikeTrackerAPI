namespace LikeTrackerAPI.Commons.DTOs
{
    public class LikeRequestDto
    {
        public int ArticleId { get; set; }
        // Optionally, add userId if you want to track unique users liking the article
    }
}
