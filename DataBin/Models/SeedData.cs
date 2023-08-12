using DataBin.Data;
using DataBin.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MVCMovie.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataBinContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DataBinContext>>()))
            {
                // Look for any movies.
                if (context.Post.Any() || context.Comment.Any() || context.Topic.Any())
                {
                    return;   // DB has been seeded
                }

                context.Post.AddRange(
                    new Post { /*Id = 1, */
                        Title = "Lorem ipsum dolor sit amet", 
                        Description = "Ea rerum voluptatem sit dolorem enim nam.", 
                        Content = "Sed eius deserunt 33 harum dolorem sed autem quae aut odio atque sit fuga nemo qui beatae dolor. Id consequatur iste ut sequi suscipit non tempora iure in assumenda error? Qui corporis dolor est dolor odio aut natus Quis sed inventore iure et quam internos! Aut eius esse 33 ipsam aliquid hic magni officiis.",
                        Stars = 30,
                        CreatedAt = DateTime.Now },
                    
                    new Post { /*Id = 2, */
                        Title = "Et sint recusandae",
                        Description = " Qui sint eveniet sed numquam dolores eum minima expedita.", 
                        Content = "Ut illo esse est dolorem doloribus non excepturi corporis ex deserunt molestias aut praesentium omnis. Et autem nihil et dolores soluta rem galisum sequi in voluptates suscipit est atque galisum. ", 
                        Stars = 40, 
                        CreatedAt = DateTime.Now },

                    new Post { /*Id = 3, */
                        Title = "At assumenda enim",
                        Description = "Aut molestiae quis est libero illo et doloribus illum non laborum.", 
                        Content = "Aut ducimus impedit sed nostrum cupiditate est earum iste eum numquam odio et rerum pariatur. Eos impedit asperiores aut voluptatem voluptatem et quis distinctio sed veniam voluptatem non quod alias qui quaerat cumque ut perferendis consequatur. ", 
                        Stars = 25,
                        CreatedAt = DateTime.Now },

                    new Post { /*Id = 4, */
                        Title = "Quis maxime aut alias nulla est galisum quos",
                        Description = "Ut galisum nobis in saepe dolorem 33 eius.",
                        Content = "Aut fugit quos 33 nulla omnis ut soluta quaerat vel possimus molestiae qui necessitatibus tempora. Aut laudantium natus non itaque Quis sed temporibus quia ut quasi excepturi aut totam molestiae?",
                        Stars = 55, 
                        CreatedAt = DateTime.Now },

                    new Post { /*Id = 5, */
                        Title = "Ea illum deserunt id repudiandae ", 
                        Content = "Est quas iure 33 consectetur eaque ab autem cupiditate sit modi quod et reprehenderit laudantium ex reprehenderit laboriosam et temporibus tempore? Aut quidem enim nam labore illum non esse galisum. ",
                        Stars = 10,
                        CreatedAt = DateTime.Now }

                );
                context.SaveChanges();

                context.Topic.AddRange(
                    new Topic { /*Id = 1, */Name = "C" },
                    new Topic { /*Id = 2, */Name = "C++" },
                    new Topic { /*Id = 3, */Name = "C#" },
                    new Topic { /*Id = 4, */Name = "Python" },
                    new Topic { /*Id = 5, */Name = "Java" },
                    new Topic { /*Id = 6, */Name = "JavaScript" },
                    new Topic { /*Id = 7, */Name = "JSON" },
                    new Topic { /*Id = 8, */Name = "TypeScript" },
                    new Topic { /*Id = 9, */Name = "Bash" },
                    new Topic { /*Id = 10, */Name = "Perl" }
                );
                context.SaveChanges();

                context.PostTopic.AddRange(
                    new PostTopic { PostId = 0, TopicId = 0 },
                    new PostTopic { PostId = 0, TopicId = 1 },
                    new PostTopic { PostId = 0, TopicId = 2 },
                    new PostTopic { PostId = 1, TopicId = 3 },
                    new PostTopic { PostId = 1, TopicId = 4 },
                    new PostTopic { PostId = 1, TopicId = 5 },
                    new PostTopic { PostId = 2, TopicId = 6 },
                    new PostTopic { PostId = 2, TopicId = 7 },
                    new PostTopic { PostId = 3, TopicId = 8 },
                    new PostTopic { PostId = 3, TopicId = 9 },
                    new PostTopic { PostId = 4, TopicId = 0 },
                    new PostTopic { PostId = 4, TopicId = 1 }
                );

                context.SaveChanges();
            }
        }
    }
}
