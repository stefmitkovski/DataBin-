using DataBin.Models;

namespace DataBin.ViewModels
{
    public class PostCommentSection
    {
        public Post? Post { get; set; }
        public Comment Comment { get; set; }
        public IList<Comment>? CommentSection { get; set;}
    }
}
