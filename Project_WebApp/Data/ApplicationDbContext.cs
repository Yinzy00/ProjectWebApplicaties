using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Image> Images { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //PostSubject
            builder.Entity<PostSubject>().HasKey(ps => new { ps.PostId, ps.SubjectId });
            builder.Entity<PostSubject>().HasOne(ps => ps.Post).WithMany(p => p.PostSubjects).OnDelete(DeleteBehavior.ClientCascade);
            //PostImage
            builder.Entity<MessageImage>().HasKey(mi=> new { mi.ImageId, mi.MessageId});
            //Images
            builder.Entity<Image>().ToTable("Image");
            builder.Entity<Image>().HasOne(i => i.User).WithMany(u => u.Images);
            builder.Entity<Image>().HasMany(i => i.MessageImages).WithOne(mi => mi.Image).OnDelete(DeleteBehavior.Restrict);
            //Message
            builder.Entity<Message>().HasMany(m => m.MessageImages).WithOne(mi => mi.Message).OnDelete(DeleteBehavior.Cascade);//.OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Message>().HasMany(m => m.Likes).WithOne(l => l.Message).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Message>().HasMany(m => m.Comments).WithOne(c=>c.Parent).OnDelete(DeleteBehavior.ClientCascade);
            //builder.Entity<Message>().HasMany(m => m.Comments).WithOne(c => c.Parent).OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<Post>().HasMany(p => p.Comments).WithOne(c => c.Parent as Post).OnDelete(DeleteBehavior.Cascade);
            //Post: Message
            builder.Entity<Post>().HasMany(p => p.PostSubjects).WithOne(ps => ps.Post).OnDelete(DeleteBehavior.Cascade);
            //Subject
            builder.Entity<Subject>().ToTable("Subject");
            builder.Entity<Subject>().HasMany(s => s.PostSubjects).WithOne(ps => ps.Subject).OnDelete(DeleteBehavior.Cascade);
            //User
            builder.Entity<User>().ToTable("User");
            //Set profile picture to null
            //builder.Entity<User>().HasOne(u => u.ProfilePicture).WithOne(pp=>pp.IsProfilePictureOf).OnDelete(DeleteBehavior.SetNull);
            //Remove all pictures on delete user
            builder.Entity<User>().HasMany(u => u.Images).WithOne(i => i.User).OnDelete(DeleteBehavior.Cascade);
            //Remove all likes on delete user
            builder.Entity<User>().HasMany(u => u.Likes).WithOne(l => l.User);//.OnDelete(DeleteBehavior.Cascade);
            //Delete all messages of that user
            builder.Entity<User>().HasMany(u => u.Messages).WithOne(m => m.User);


            builder.Entity<Like>().ToTable("Like");

            //builder.Entity<Comment>().HasOne(c => c.Parent).WithMany(p => p.Comments).OnDelete(DeleteBehavior.Cascade);
        }

        //public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
        //      new ConsoleLoggerProvider((_, __) => true, true)
        //});

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            //IServiceCollection serviceCollection = new ServiceCollection();
            //serviceCollection.AddLogging(builder => builder
            //    .AddConsole()
            //    .AddFilter(level => level >= LogLevel.Debug)
            //);
            //var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();

            //optionsBuilder.UseLoggerFactory(loggerFactory);
        }
    }
}
