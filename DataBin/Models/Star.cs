namespace DataBin.Models
{
    public class Star
    {
        public int Id { get; set; }
        public string User { get; set; }
        
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
