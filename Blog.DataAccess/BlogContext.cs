using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;


//Imena za diplomski blog : warlog,battlog
namespace Blog.DataAccess
{
    public class BlogContext : DbContext
    {
        //public IApplicationUser User { get; } za sada nije nuzno
        public BlogContext(DbContextOptions options = null) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=blog;Integrated Security=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
        // Servis za pristup bazi je registrovan u Startup-u kroz ContainerExtension metod AddBlogContext!
        //public override int SaveChanges()
        //{
        //    foreach (var entry in this.ChangeTracker.Entries())
        //    {
        //        if (entry.Entity is Entity e)
        //        {
        //            switch (entry.State)
        //            {
        //                case EntityState.Added:
        //                    e.IsActive = true;
        //                    e.CreatedAt = DateTime.UtcNow;
        //                    break;
        //                case EntityState.Modified:
        //                    e.UpdatedAt = DateTime.UtcNow;
        //                    e.UpdatedBy = User?.Identity;
        //                    break;
        //            }
        //        }
        //    }

        //    return base.SaveChanges();
        //}
        // Entity u ovom projektu neka UpdatedAt, IsActive, CreatedAt i UpdatedBy ....



        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<VoteType> VoteTypes { get; set; }
        public DbSet<UseCase> UseCases { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
        public DbSet<BlogPostImage> BlogPostImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }

    }
}
