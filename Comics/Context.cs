using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comics.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Comics
{
    public class Context : DbContext
    {
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Comic> Comics { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<AutoresYComics> AutoresYComics { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=comic;Trusted_Connection=True;");
        }
    }
}
