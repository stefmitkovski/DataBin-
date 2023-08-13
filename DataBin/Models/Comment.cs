using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBin.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastUpdatedAt { get; set; }

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
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
