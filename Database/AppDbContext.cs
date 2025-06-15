using Library.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Library.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<IssueBook> IssueBooks { get; set; }
        public DbSet<ReturnBook> ReturnBooks { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<SearchBook> SearchBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Required for Identity

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Title).IsRequired();
                entity.Property(b => b.Author).IsRequired();
                entity.Property(b => b.ISBN).IsRequired().HasMaxLength(13);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.FullName).IsRequired();
                entity.Property(m => m.Email).IsRequired();
            });

            modelBuilder.Entity<IssueBook>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.IssueDate).IsRequired();
                entity.Property(i => i.DueDate).IsRequired();

                entity.HasOne(i => i.Book)
                      .WithMany()
                      .HasForeignKey(i => i.BookId);

                entity.HasOne(i => i.Member)
                      .WithMany()
                      .HasForeignKey(i => i.MemberId);
            });

            modelBuilder.Entity<ReturnBook>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.ReturnDate).IsRequired();

                entity.HasOne(r => r.IssueBook)
                      .WithOne()
                      .HasForeignKey<ReturnBook>(r => r.IssueBookId);
            });

            modelBuilder.Entity<Fine>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Amount).IsRequired();

                entity.HasOne(f => f.ReturnBook)
                      .WithMany()
                      .HasForeignKey(f => f.ReturnBookId);
            });

            modelBuilder.Entity<SearchBook>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Keyword).IsRequired();
                entity.Property(s => s.SearchedOn).IsRequired();
            });
        }
    }
}
