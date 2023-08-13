using DataBin.Models;

namespace DataBin.ViewModels
{
    public class ListingPosts
    {
        public IList<Post> Posts { get; set; }
        public int PageNumber = 1;
    }
}
