using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
namespace WcfServiceLibrary
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Shoping_cart> Shoping_cart { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          // string baseDirectory = AppDomain.CurrentDomain.BaseDirectory; 
          //string relativePath = @"DB\EasyLibraryDB.mdf";
          //string absolutePath = Path.Combine(baseDirectory, relativePath); 
          string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\10\\Desktop\\roeDatabase.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";
          optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
