using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBin.Models
{
    public class Post
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }
        public Language? Language { get; set; }

        [Required]
        public int Stars { get; set; } = 0;

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public string TimePassedCreation
        {
            get
            {

                if (this.CreatedAt == null)
                {
                    return "No created time available";
                }

                TimeSpan timeSpan = (TimeSpan)(DateTime.Now - this.CreatedAt);

                if (timeSpan.TotalSeconds < 60)
                {
                    return $"{timeSpan.Seconds} seconds ago";
                }
                else if (timeSpan.TotalMinutes < 60)
                {
                    return $"{timeSpan.Minutes} minutes ago";
                }
                else if (timeSpan.TotalHours < 24)
                {
                    return $"{timeSpan.Hours} hours ago";
                }
                else
                {
                    return $"{timeSpan.Days} days ago";
                }
            }
        }

        [DataType(DataType.DateTime)]
        public DateTime? LastUpdatedAt { get; set; }

        [NotMapped]
        public string TimePassedUpdated
        {
            get
            {
                if (this.LastUpdatedAt == null)
                {
                    return "This post hasn't been updated";
                }

                TimeSpan timeSpan = (TimeSpan)(DateTime.Now - this.LastUpdatedAt);

                if (timeSpan.TotalSeconds < 60)
                {
                    return $"{timeSpan.Seconds} seconds ago";
                }
                else if (timeSpan.TotalMinutes < 60)
                {
                    return $"{timeSpan.Minutes} minutes ago";
                }
                else if (timeSpan.TotalHours < 24)
                {
                    return $"{timeSpan.Hours} hours ago";
                }
                else
                {
                    return $"{timeSpan.Days} days ago";
                }
            }
        }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostTopic>? PostTopics { get; set; }
    }
}
