using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comics.Controladores;
using Comics.Funciones;

namespace Comics.Modelos
{
    public class Comic : IentidadBD
    {
        public Comic()
        {
        }

        public Comic(string titulo, string descripcion, DateTime fecha, int numeroPaginas, Categoria categoria)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Fecha = fecha;
            NumeroPaginas = numeroPaginas;
            Categoria = categoria;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; } = DateTime.MinValue;
        public int NumeroPaginas { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int? EditorialId { get; set; }
        public Editorial? Editorial { get; set; }
        public IList<AutoresYComics> Autores { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Comic comic &&
                   Titulo == comic.Titulo &&
                   Descripcion == comic.Descripcion &&
                   Fecha == comic.Fecha &&
                   NumeroPaginas == comic.NumeroPaginas &&
                   EqualityComparer<Categoria>.Default.Equals(Categoria, comic.Categoria);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Titulo, Descripcion, Fecha, NumeroPaginas, Categoria);
        }

        public override string ToString()
        {
            return "Id: " + Id + ", titulo: " + Titulo + ", descripcion: " + Descripcion + ", fecha: " + Fecha.ToString("MM/dd/yyyy") + ", numero de paginas: " + NumeroPaginas + ", categoria: " + Categoria +", editorial : " + Editorial;
        }


    }
}
