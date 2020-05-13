using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server = localhost\\SQLEXPRESS; Database=ConsoleApp1;Trusted_Connection=True;").UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().HasMany(b => b.Posts).WithOne()
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
            //modelBuilder.Entity<Blog>().OwnsMany(b => b.Posts).WithOwner(); //this works 
        }
    }

    public class Blog
    {
        public int Id { get; set; }

        private readonly List<Post> _posts = new List<Post>();
        public virtual IReadOnlyList<Post> Posts => _posts.ToList();

        public void SetPosts(List<Post> posts)
        {
            //some business logic
            _posts.Clear();
            _posts.AddRange(posts);
        }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Content { get; set; }
    }
}
