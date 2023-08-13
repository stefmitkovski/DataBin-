using DataBin.Models;

namespace DataBin.ViewModels
{
    public class HomePagePosts
    {
        public IList<Post> MostStared { get; set; }
        public IList<Post> MostRecent { get; set; }
    }
}
