using DataBin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataBin.ViewModels
{
    public class ListingPosts
    {
        public IList<Post> Posts { get; set; }
        public SelectList Topics { get; set; }
        public SelectList Languages { get; set; }
        public string PostTopic { get; set; }
        public string Language { get; set; }
        public string SearchString { get; set; }
    }
}
