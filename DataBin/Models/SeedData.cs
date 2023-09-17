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
                    new Language { /*Id = 7, */Name = "Rust" },
                    new Language { /*Id = 8, */Name = "Perl" },
                    new Language { /*Id = 9, */Name = "Lua" },
                    new Language { /*Id = 10, */Name = "GO" }
                );

                context.SaveChanges();

                context.Post.AddRange(
                    new Post
                    { /*Id = 1, */
                        Title = "Intro into C programing",
                        Content = "#include <stdio.h>\r\n\r\nint main() {\r\n    printf(\"Hello, World!\\n\");\r\n    return 0;\r\n}\r\n",
                        Stars = 30,
                        LanguageId = 1,
                        CreatedAt = DateTime.Now
                    },

                    new Post
                    { /*Id = 2, */
                        Title = "Basic C++ program",
                        Content = "#include <iostream>\r\n\r\nint main() {\r\n    std::cout << \"Hello, World!\" << std::endl;\r\n    return 0;\r\n}\r\n",
                        Stars = 40,
                        LanguageId = 2,
                        CreatedAt = DateTime.Now
                    },

                    new Post
                    { /*Id = 3, */
                        Title = "Be sharp in C#",
                        Content = "using System;\r\n\r\nclass Program {\r\n    static void Main() {\r\n        Console.WriteLine(\"Hello, World!\");\r\n    }\r\n}\r\n",
                        Stars = 25,
                        LanguageId = 3,
                        CreatedAt = DateTime.Now
                    },

                    new Post
                    { /*Id = 4, */
                        Title = "Python for noobs",
                        Content = "print(\"Hello, World!\")\r\n",
                        Stars = 55,
                        LanguageId = 4,
                        CreatedAt = DateTime.Now
                    },

                    new Post
                    { /*Id = 5, */
                        Title = "Your first Java program",
                        Content = "public class HelloWorld {\r\n    public static void main(String[] args) {\r\n        System.out.println(\"Hello, World!\");\r\n    }\r\n}\r\n",
                        Stars = 10,
                        LanguageId = 5,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 6, */
                        Title = "Learn web programing with Javascript",
                        Content = "console.log(\"Hello, World!\");\r\n",
                        Stars = 45,
                        LanguageId = 6,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 7, */
                        Title = "Rust in Action",
                        Content = "fn main() {\r\n    println!(\"Hello, World!\");\r\n}\r\n",
                        Stars = 80,
                        LanguageId = 7,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 8, */
                        Title = "Learn the basics in Perl",
                        Content = "print(\"Hello, World!\\n\");\r\n",
                        Stars = 70,
                        LanguageId = 8,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 9, */
                        Title = "Game development in Lua",
                        Content = "print(\"Hello, World!\")\r\n",
                        Stars = 20,
                        LanguageId = 9,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 10, */
                        Title = "Golang programming",
                        Content = "package main\r\n\r\nimport \"fmt\"\r\n\r\nfunc main() {\r\n    fmt.Println(\"Hello, World!\")\r\n}\r\n",
                        Stars = 18,
                        LanguageId = 10,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 11, */
                        Title = "Advanced C",
                        Content = "#include <stdio.h>\r\n\r\nint main() {\r\n    int num1 = 10;\r\n    int num2 = 20;\r\n    int sum = num1 + num2;\r\n    \r\n    printf(\"The sum of %d and %d is %d\\n\", num1, num2, sum);\r\n    \r\n    return 0;\r\n}\r\n",
                        Stars = 44,
                        LanguageId = 1,
                        CreatedAt = DateTime.Now
                    },
                    new Post
                    { /*Id = 12, */
                        Title = "Advanced C++",
                        Content = "#include <iostream>\r\n\r\nint main() {\r\n    int num1 = 10;\r\n    int num2 = 20;\r\n    int sum = num1 + num2;\r\n    \r\n    std::cout << \"The sum of \" << num1 << \" and \" << num2 << \" is \" << sum << std::endl;\r\n    \r\n    return 0;\r\n}\r\n",
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
