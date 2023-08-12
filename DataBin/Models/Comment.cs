using System.ComponentModel.DataAnnotations;

namespace DataBin.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastUpdatedAt { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
