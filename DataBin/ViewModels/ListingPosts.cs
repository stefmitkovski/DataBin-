using DataBin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataBin.ViewModels
{
    public class ListingPosts
    {
        public IList<Post> Posts { get; set; }
        public int PageNumber = 1;
        public Boolean Flag = true;
        //    public SelectList? Topics { get; set; }
        //    public string? PostTopic { get; set; }
        //    public string? SearchString { get; set; }
    }
}
