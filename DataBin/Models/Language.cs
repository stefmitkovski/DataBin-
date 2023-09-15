namespace DataBin.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
