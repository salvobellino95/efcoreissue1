using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EFGetStarted
{
    class Program
    {
        static void Main()
        {
            using (var db = new BloggingContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var blog = new Blog();
                blog.SetPosts(new List<Post>() { new Post() { Content = "post1" }, new Post() { Content = "post2" } });
                db.Blogs.Add(blog);
                db.SaveChanges();
            }

            using (var db = new BloggingContext())
            {
                var blog = db.Blogs.First();
                blog.SetPosts(new List<Post>() { new Post() { Content = "post3" }, new Post() { Content = "post4" } });
                db.SaveChanges();

                Debug.Assert(blog.Posts.Count == 2); //fails, it contains also the removed posts post1,post2
            }
        }
    }
}
