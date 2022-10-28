using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Tamirci.Models
{
    public class Context:DbContext
    {
        public DbSet<Yorumlar> Yorumlars { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Hakkımızda> Hakkımızdas { get; set; }
        public DbSet<Mesajlar> Mesajlars { get; set; }
        public DbSet<Tamirciler> Tamircilers { get; set; }
    }
}