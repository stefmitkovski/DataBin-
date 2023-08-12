namespace DataBin.Models
{
    public class PostTopic
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post? Post { get; set; }
        public int TopicId { get; set; }
        public Topic? Topic { get; set; }
    }
}
