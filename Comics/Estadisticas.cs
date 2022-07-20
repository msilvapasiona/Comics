using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comics;
using Microsoft.EntityFrameworkCore;
namespace Comics
{
    public class Estadisticas
    {
        private static Context Contexto { get; set; } = new Context();
        public static void NumeroComicsCategoria()
        {
            Console.WriteLine();
            foreach (var categoria in Contexto.Categorias.ToList().GroupBy(x => x.Nombre))
            {
                int numeroComics = Contexto.Comics.Where(x => x.Categoria.Nombre.Equals(categoria.Key)).Count();
                Console.WriteLine("Categoria : " + categoria.Key + ", numero total de comics:" + numeroComics);
            }
        }
        public static void NumeroComicsAutor()
        {
            Console.WriteLine();
            var autores = Contexto.Autores.Include(x => x.Comics).ToList();
            foreach (var item in autores)
            {
                Console.WriteLine("Nombre: " + item.Nombre + ", numero de comics: " + item.Comics.Count());
            }
        }
        public static void ComicsConMayorNumeroPaginas(int limite)
        {
            Console.WriteLine();
            var comicsConMasPaginas = Contexto.Comics.OrderByDescending(x => x.NumeroPaginas).Take(limite);
            foreach (var item in comicsConMasPaginas)
            {
                Console.WriteLine(item);
            }
        }
        public static void AutoresPorNacionalidad()
        {
            Console.WriteLine();
            foreach (var autor in Contexto.Autores.ToList().GroupBy(x => x.Nacionalidad))
            {
                int numeroAutores = Contexto.Autores.Where(x => x.Nacionalidad.Equals(autor.Key)).Count();
                Console.WriteLine("Nacionalidad : " + autor.Key + ", numero total de autores:" + numeroAutores);
            }
        }
    }
}