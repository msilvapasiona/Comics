using Comics.Controladores;
using Comics.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Comics.Funciones
{
    [NotMapped]
    public class FuncionesComic:IFunciones
    {
        public FuncionesComic()
        {
            this.funciones = new ComicDAO<Comic>(Contexto, Contexto.Comics);
        }
        public FuncionesComic(ICrud<Comic> funciones)
        {
            this.funciones = funciones;
        }
        public ICrud<Comic> funciones { get; set; } 

        public Context Contexto { get; } = new Context();

        public void Añadir()
        {
            Comic comic = new Comic();
            Console.WriteLine();

            Console.Write("Introduce el titulo del comic: ");
            comic.Titulo = Console.ReadLine();

            Console.Write("Introduce la descripcion del comic:");
            comic.Descripcion = Console.ReadLine();

            Console.Write("Introduce la fecha del comic(MM/dd/yyyy):");
            string fecha = Console.ReadLine();
            comic.Fecha = DateTime.Parse(fecha);

            Console.Write("Introduce el numero de paginas:");
            int numeroPaginas = 0;
            int.TryParse(Console.ReadLine(), out numeroPaginas);
            comic.NumeroPaginas = numeroPaginas;

            bool consulta = false;
            int idCategoria = 0;
            while (!consulta)
            {
                Console.Write("Introduce el id de tu categoria: ");
                int.TryParse(Console.ReadLine(), out idCategoria);
                Context contexto2 = new Context();
                consulta = Contexto.Categorias.Any(x => x.Id == idCategoria);
            }
            comic.CategoriaId = idCategoria;


            consulta = false;
            int idEditorial= -1;
            while (!consulta && idEditorial != 0)
            {
                Console.Write("Introduce el id de tu editorial (0 para no introducir una editorial): ");
                int.TryParse(Console.ReadLine(), out idEditorial);
                Context contexto2 = new Context();
                consulta = Contexto.Editoriales.Any(x => x.Id == idEditorial);
            }
            if (idEditorial != 0)
            {
                comic.EditorialId = idEditorial;
            }

            comic = funciones.Guardar(comic);
            Console.WriteLine(comic != null ? $"Comic creado con el id {comic.Id}" : "No se ha podido guardar este comic, puede que ya exista un comic igual.");

        }
        public void Mostrar()
        {
            if (funciones.Mostrar().Count() > 0)
            {
                foreach (var item in funciones.Mostrar())
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("Todavia no hay ninguna editorial añadida.");
            }
        }
        public void Modificar()
        {
            Comic auxiliar = new Comic();

            Console.WriteLine();
            Console.WriteLine("Introduzca el id a modificar: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);
            auxiliar = funciones.ComprobarExistencia(id);

            Console.WriteLine("Introduzca el nuevo titulo (intro para no modificar): ");
            string titulo = Console.ReadLine();
            auxiliar.Titulo = titulo.Equals("") ? auxiliar.Titulo: titulo;

            Console.WriteLine("Introduzca la nueva descripcion (intro para no modificar): ");
            string descripcion = Console.ReadLine();
            auxiliar.Descripcion = descripcion.Equals("") ? auxiliar.Descripcion : descripcion;

            Console.Write("Introduce la fecha del comic(MM/dd/yyyy) (intro para no modificar):");
            string fecha = Console.ReadLine();
            auxiliar.Fecha = fecha.Equals("") ? auxiliar.Fecha : DateTime.Parse(fecha);

            Console.Write("Introduce el numero de paginas (-1 para no modificar):");
            int numeroPaginas = 0;
            int.TryParse(Console.ReadLine(), out numeroPaginas);
            auxiliar.NumeroPaginas = numeroPaginas == -1 ? auxiliar.NumeroPaginas : numeroPaginas;
            
            bool consulta = false;
            int idCategoria = 0;
            while (!consulta && idCategoria != -1)
            {
                Console.Write("Introduce el id de tu categoria(-1 para no modificar): ");
                int.TryParse(Console.ReadLine(), out idCategoria);
                consulta = Contexto.Categorias.Any(x => x.Id == idCategoria);
            }
            auxiliar.CategoriaId = idCategoria == -1 ? auxiliar.CategoriaId:idCategoria;

            consulta = false;
            int idEditorial = 0;
            while (!consulta && idEditorial != -1)
            {
                Console.Write("Introduce el id de tu editorial (-1 para no introducir una editorial): ");
                int.TryParse(Console.ReadLine(), out idEditorial);
                Context contexto2 = new Context();
                consulta = Contexto.Editoriales.Any(x => x.Id == idEditorial);
            }
            auxiliar.EditorialId = idEditorial == -1 ? auxiliar.EditorialId :idEditorial;

            auxiliar = funciones.Modificar(auxiliar);
            Console.WriteLine(auxiliar != null ? "Se ha modificado con exito" : "No se ha podido modificar este comic.");
        }
        public void Eliminar()
        {
            Console.WriteLine();
            Console.WriteLine("Introduzca el id a eliminar: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            Comic auxiliar = funciones.ComprobarExistencia(id);
            if (auxiliar != null)
            {
                Console.WriteLine($"Se Borrará el comic {auxiliar.Titulo}. ¿Está seguro?(s/n)");
                string respuesta = Console.ReadLine();
                if (respuesta.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    auxiliar = funciones.Eliminar(auxiliar.Id);
                    Console.WriteLine("Comic eliminada con exito.");
                }
                else
                {
                    Console.WriteLine("Operacion Cancelada.");
                }
            }
            else
            {
                Console.WriteLine("No existe ningun Comic con este id.");
            }
        }
    }    
}
