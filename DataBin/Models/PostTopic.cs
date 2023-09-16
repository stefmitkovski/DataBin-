using System.ComponentModel.DataAnnotations;

namespace DataBin.Models
{
    public class PostTopic
    {
        public int Id { get; set; }
        [Required]
        public int PostId { get; set; }
        public Post? Post { get; set; }
        [Required]
        public int TopicId { get; set; }
        public Topic? Topic { get; set; }
    }
}
