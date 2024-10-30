using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LikeTrackerAPI.Entities
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public int LikesCount { get; set; } = 0; 
    }
}
