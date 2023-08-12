using System.ComponentModel.DataAnnotations;

namespace DataBin.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 10)]
        public string Title { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        public int Stars { get; set; } = 0;

        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastUpdatedAt { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostTopic>? PostTopics { get; set; }
    }
}
