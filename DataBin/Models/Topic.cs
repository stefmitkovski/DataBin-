using ServiceStack.DataAnnotations;

namespace DataBin.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public ICollection<PostTopic>? PostTopics { get; set; }
    }
}
