using DataBin.Data;
using DataBin.Models;
using Microsoft.EntityFrameworkCore;

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
                if (context.Post.Any() || context.Comment.Any() || context.Topic.Any() || context.Language.Any())
                {
                    return;   // DB has been seeded
                }

                context.Language.AddRange(
                    new Language { /*Id = 1, */Name = "C" },
                    new Language { /*Id = 2, */Name = "C++" },
                    new Language { /*Id = 3, */Name = "C#" },
                    new Language { /*Id = 4, */Name = "Python" },
                    new Language { /*Id = 5, */Name = "Java" },
                    new Language { /*Id = 6, */Name = "JavaScript" },
                    new Language { /*Id = 7, */Name = "JSON" },
                    new Language { /*Id = 8, */Name = "TypeScript" },
                    new Language { /*Id = 9, */Name = "Perl" },
                    new Language { /*Id = 10, */Name = "None" }
                );

                context.SaveChanges();

                context.Post.AddRange(
                    new Post
                    { /*Id = 1, */
                        Title = "Lorem ipsum dolor sit amet",
                        Description = "Ea rerum voluptatem sit dolorem enim nam.",
                        Content = "Sed eius deserunt 33 harum dolorem sed autem quae aut odio atque sit fuga nemo qui beatae dolor. Id consequatur iste ut sequi suscipit non tempora iure in assumenda error? Qui corporis dolor est dolor odio aut natus Quis sed inventore iure et quam internos! Aut eius esse 33 ipsam aliquid hic magni officiis.",
                        Stars = 30,
                        LanguageId = 1,
                        CreatedAt = DateTime.Now
                    },

                    new Post
                    { /*Id = 2, */
                        Title = "Et sint recusandae",
                        Description = " Qui sint eveniet sed numquam dolores eum minima expedita.",
                        Content = "Ut illo esse est dolorem doloribus non excepturi corporis ex deserunt molestias aut praesentium omnis. Et autem nihil et dolores soluta rem galisum sequi in voluptates suscipit est atque galisum. ",
                        Stars = 40,
                        LanguageId = 2,
                        CreatedAt = DateTime.Now
                    },

                    new Post
                    { /*Id = 3, */
                        Title = "At assumenda enim",
                        Description = "Aut molestiae quis est libero illo et doloribus illum non laborum.",
                        Content = "Aut ducimus impedit sed nostrum cupiditate est earum iste eum numquam odio et rerum pariatur. Eos impedit asperiores aut voluptatem voluptatem et quis distinctio sed veniam voluptatem non quod alias qui quaerat cumque ut perferendis consequatur. ",
                        Stars = 25,
                        LanguageId = 3,
                        CreatedAt = DateTime.Now
                    },

                    new Post
                    { /*Id = 4, */
                        Title = "Quis maxime aut alias nulla est galisum quos",
                        Description = "Ut galisum nobis in saepe dolorem 33 eius.",
                        Content = "Aut fugit quos 33 nulla omnis ut soluta quaerat vel possimus molestiae qui necessitatibus tempora. Aut laudantium natus non itaque Quis sed temporibus quia ut quasi excepturi aut totam molestiae?",
                        Stars = 55,
                        LanguageId = 4,
                        CreatedAt = DateTime.Now
                    },

                    new Post
                    { /*Id = 5, */
                        Title = "Ea illum deserunt id repudiandae ",
                        Content = "Est quas iure 33 consectetur eaque ab autem cupiditate sit modi quod et reprehenderit laudantium ex reprehenderit laboriosam et temporibus tempore? Aut quidem enim nam labore illum non esse galisum. ",
                        Stars = 10,
                        LanguageId = 5,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 6, */
                        Title = "Quo dolores porro qui sapiente dolores",
                        Description = "Sit autem ipsa eos dicta odio cum internos laboriosam qui quos placeat eos reiciendis vitae.",
                        Content = "Ab molestias doloremque aut deleniti voluptatum eos dolor eligendi sed illum earum et exercitationem sapiente rem accusamus unde ut accusantium expedita. ",
                        Stars = 45,
                        LanguageId = 6,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 7, */
                        Title = "Ut repudiandae repellat ",
                        Description = "Sit eligendi delectus ut error tempore sed repudiandae voluptate",
                        Content = "Qui soluta quidem ut dolore quod est eligendi alias vel labore deserunt quo quidem nesciunt. Est alias mollitia qui amet sint et sunt galisum sed tempora aliquid.",
                        Stars = 80,
                        LanguageId = 7,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 8, */
                        Title = "Et accusamus quia vel tempora rerum",
                        Description = "At galisum placeat qui provident aliquam hic sunt illo. In velit cumque ea harum quam.",
                        Content = "Ut consequatur praesentium non commodi dolor et totam dolore sit eius vero sed aliquam eaque quo autem obcaecati. ",
                        Stars = 70,
                        LanguageId = 8,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 9, */
                        Title = "Aut iure provident vel sequi",
                        Description = "Qui totam rerum aut adipisci alias qui ipsum natus ab iste consequatur aut soluta omnis",
                        Content = "Non nemo tempore At autem numquam id dicta iusto id rerum tempora ut quos soluta et quod molestiae ut accusamus incidunt",
                        Stars = 20,
                        LanguageId = 9,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 10, */
                        Title = "In quia voluptatem aut ",
                        Description = "Non nostrum itaque et tempora rerum qui asperiores beatae",
                        Content = "Ut deserunt excepturi aut voluptatem delectus est exercitationem reprehenderit et deserunt quis eum dolores ducimus.",
                        Stars = 18,
                        LanguageId = 10,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 11, */
                        Title = "Ea id repudiandae ",
                        Description = "At itaque perferendis et cumque iste.",
                        Content = "Non autem unde qui eligendi deleniti ut quia dolorem id optio ducimus. Aut culpa rerum et dolores voluptatem et ullam laborum At modi esse. ",
                        Stars = 44,
                        LanguageId = 1,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 12, */
                        Title = "Vel quisquam consequatur nam laboriosam ",
                        Description = "Et doloribus iure eum deleniti consectetur et excepturi voluptas et facere consequatur.",
                        Content = "Est rerum aliquid qui maiores sunt est doloribus perferendis ea molestiae modi sit quam velit ut quidem galisum. ",
                        Stars = 36,
                        LanguageId = 2,
                        CreatedAt = DateTime.Now
                    }
                );
                context.SaveChanges();

                context.Topic.AddRange(
                    new Topic { /*Id = 1, */Name = "Programming" },
                    new Topic { /*Id = 2, */Name = "Networking" },
                    new Topic { /*Id = 3, */Name = "Game Development" },
                    new Topic { /*Id = 4, */Name = "Embedded Programming" },
                    new Topic { /*Id = 5, */Name = "DevOps" },
                    new Topic { /*Id = 6, */Name = "Core Dumps" },
                    new Topic { /*Id = 7, */Name = "Android Development" },
                    new Topic { /*Id = 8, */Name = "IOS Development" }
                );

                context.SaveChanges();

                context.PostTopic.AddRange(
                    new PostTopic { PostId = 1, TopicId = 1 },
                    new PostTopic { PostId = 2, TopicId = 2 },
                    new PostTopic { PostId = 3, TopicId = 3 },
                    new PostTopic { PostId = 4, TopicId = 4 },
                    new PostTopic { PostId = 5, TopicId = 5 },
                    new PostTopic { PostId = 6, TopicId = 6 },
                    new PostTopic { PostId = 7, TopicId = 7 },
                    new PostTopic { PostId = 8, TopicId = 8 },
                    new PostTopic { PostId = 9, TopicId = 1 },
                    new PostTopic { PostId = 10, TopicId = 2 },
                    new PostTopic { PostId = 11, TopicId = 3 },
                    new PostTopic { PostId = 12, TopicId = 4 }
                );

                context.SaveChanges();
            }
        }
    }
}
