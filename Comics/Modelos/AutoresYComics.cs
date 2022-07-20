using Comics.Controladores;
using Comics.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comics.Modelos
{
    public class AutoresYComics: IentidadBD
    {

        public AutoresYComics()
        {
        }

        public AutoresYComics(Autor autor, Comic comic, string rol)
        {
            Autor = autor;
            Comic = comic;
            Rol = rol;
        }

        public AutoresYComics(int autorId, int comicId, string rol)
        {
            AutorId = autorId;
            ComicId = comicId;
            Rol = rol;
        }

        public int Id { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
        public int ComicId { get; set; }
        public Comic Comic { get; set; }
        public string Rol { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is AutoresYComics comics &&
                   EqualityComparer<Autor>.Default.Equals(Autor, comics.Autor) &&
                   EqualityComparer<Comic>.Default.Equals(Comic, comics.Comic) &&
                   Rol == comics.Rol;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Autor, Comic, Rol);
        }

        public override string ToString()
        {
            return "Id: " + Id + ",nombre del autor: " + Autor.Nombre + ",id del autor: " + AutorId + ",nombre del comic: " + Comic.Titulo + ",id del comic: " + Comic.Id + ", rol: " + Rol;
        }
    }
}
