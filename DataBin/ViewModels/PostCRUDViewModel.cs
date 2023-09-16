using DataBin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataBin.ViewModels
{
    public class PostCRUDViewModel
    {
        public Post Post { get; set; }
        public SelectList Languages { get; set; }
        public int Language { get; set; }
        public IEnumerable<int>? SelectedTopics { get; set; }
        public IEnumerable<SelectListItem>? TopicList { get; set; }
    }
}
