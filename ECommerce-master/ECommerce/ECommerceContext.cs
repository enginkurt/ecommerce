using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            //DB BAĞLANTISI VB DATABASE INSTANCE'INI İLGİLENDİREN İNCE AYARLAR
            //dbContextOptionsBuilder.UseSqlServer("Server=127.0.0.1;Database=ECommerce;User Id=sa;Password=123;");
            dbContextOptionsBuilder.UseSqlServer(@"Server=.;Database=ECommerce;Trusted_Connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //DATABASE ŞEMASI UYGULANIRKEN KULLANILACAK KURAL SETLERİ
            modelBuilder.Entity<Category>().HasData(new Category() {
                Id = 1,
                Name = "Elektronik",
                Description = "Ev elektriğine dair herşey."
            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 2,
                Name = "Beyaz Eşya",
                Description = "Mutfak elektroniği."
            });
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 3,
                Name = "Tekstil",
                Description = "Gardropunuzu biz dolduruyoruz."
            });
            modelBuilder.Entity<State>().HasData(new State()
            {
                Id = 1,
                Name = "Aktif"
            });
            modelBuilder.Entity<State>().HasData(new State()
            {
                Id = 2,
                Name = "Pasif"
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}