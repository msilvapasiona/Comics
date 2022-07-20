using Comics.Controladores;
using Comics.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comics.Funciones
{
    [NotMapped]
    public class FuncionesAutoresYComics: IFunciones
    {
        public FuncionesAutoresYComics()
        {
            this.funciones = new AutorComicDAO<AutoresYComics>(Contexto, Contexto.AutoresYComics);
        }
        public FuncionesAutoresYComics(ICrud<AutoresYComics> funciones)
        {
            this.funciones = funciones;
        }

        public ICrud<AutoresYComics> funciones { get; set; } 
        public Context Contexto { get; } = new Context();

        public void Añadir()
        {
            Console.Write("Asignar por id(1) o nombres(2): ");
            int opcion = 0;
            int.TryParse(Console.ReadLine(), out opcion);
            if (opcion == 1)
            {
                AutoresYComics autorYComic = new AutoresYComics();
                Console.WriteLine();

                bool consulta = false;
                int idAutor = 0;
                while (!consulta)
                {
                    Console.Write("Introduce el id del autor: ");
                    int.TryParse(Console.ReadLine(), out idAutor);
                    consulta = Contexto.Autores.Any(x => x.Id == idAutor);
                }
                autorYComic.AutorId = idAutor;

                consulta = false;
                int idComic = 0;
                while (!consulta)
                {
                    Console.Write("Introduce el id del comic al que quieres asignar este autor: ");
                    int.TryParse(Console.ReadLine(), out idComic);
                    consulta = Contexto.Comics.Any(x => x.Id == idComic);
                }
                autorYComic.ComicId = idComic;

                Console.Write("Introduce el Rol que desempeña este autor en este comic: ");
                autorYComic.Rol = Console.ReadLine();

                autorYComic = funciones.Guardar(autorYComic);
                Console.WriteLine(autorYComic != null ? $"Asignacion añadida con el id {autorYComic.Id}"
                                                      : "No se ha podido guardar esta asignacion, puede que ya exista.");
            }
            else
            {
                AñadirPorNombre();
            }
        }
        public void AñadirPorNombre()
        {
            AutoresYComics autorYComic = new AutoresYComics();
            Console.WriteLine();

            bool consulta = false;
            string nombreAutor = "";
            while (!consulta)
            {
                Console.Write("Introduce el nombre del autor: ");
                nombreAutor = Console.ReadLine();
                consulta = Contexto.Autores.Any(x => x.Nombre.Equals(nombreAutor));
            }
            autorYComic.AutorId = Contexto.Autores.Where(x => x.Nombre.Equals(nombreAutor)).First().Id;

            consulta = false;
            string nombreComic = "";
            while (!consulta)
            {
                Console.Write("Introduce el nombre del comic al que quieres asignar este autor: ");
                nombreComic = Console.ReadLine();
                consulta = Contexto.Comics.Any(x => x.Titulo.Equals(nombreComic));
            }
            autorYComic.ComicId = Contexto.Comics.Where(x => x.Titulo.Equals(nombreComic)).First().Id;

            Console.Write("Introduce el Rol que desempeña este autor en este comic: ");
            autorYComic.Rol = Console.ReadLine();

            autorYComic = funciones.Guardar(autorYComic);
            Console.WriteLine(autorYComic != null ? $"Asignacion añadida con el id {autorYComic.Id}"
                                                  : "No se ha podido guardar esta asignacion, puede que ya exista.");
        }
        public void Eliminar()
        {
            Console.WriteLine();
            Console.Write("Introduce el id de la asignacion que quieres eliminar: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);

            AutoresYComics auxiliar = funciones.ComprobarExistencia(id);
            if (auxiliar != null)
            {
                Console.WriteLine($"Se Borrará las asginacion del autor: {auxiliar.Autor.Nombre} con el comic {auxiliar.Comic.Titulo}. ¿Está seguro?(s/n)");
                string respuesta = Console.ReadLine();
                if (respuesta.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    auxiliar = funciones.Eliminar(auxiliar.Id);
                    Console.WriteLine("Asignacion eliminada con exito.");
                }
                else
                {
                    Console.WriteLine("Operacion Cancelada.");
                }
            }
            else
            {
                Console.WriteLine("No existe ninguna asignacion con este id.");
            }
        }
        public void Modificar()
        {
            Console.Write("Modificar por id(1) o por nombres(2):");
            int opcion = 0;
            int.TryParse(Console.ReadLine(), out opcion);
            if (opcion == 1)
            {
                AutoresYComics auxiliar = new AutoresYComics();
                Console.WriteLine();
                Console.Write("Introduce el id de la asignacion que quieres modificar: ");
                int idAsignacion = 0;
                int.TryParse(Console.ReadLine(), out idAsignacion);
                auxiliar = funciones.ComprobarExistencia(idAsignacion);

                bool consulta = false;
                int idAutor = 0;
                while (!consulta && idAutor != -1)
                {
                    Console.Write("Introduce el id del autor(-1 para no modificar): ");
                    int.TryParse(Console.ReadLine(), out idAutor);
                    consulta = Contexto.Autores.Any(x => x.Id == idAutor);
                }
                auxiliar.AutorId = idAutor == -1 ? auxiliar.AutorId:idAutor;


                consulta = false;
                int idComic = 0;
                while (!consulta && idComic != -1)
                {
                    Console.Write("Introduce el id del comic(-1 para no modificar): ");
                    int.TryParse(Console.ReadLine(), out idComic);
                    consulta = Contexto.Comics.Any(x => x.Id == idComic);
                }
                auxiliar.ComicId = idComic == -1 ? auxiliar.ComicId:idComic;



                Console.WriteLine("Introduce el rol que tiene el autor (intro para no modificar): ");
                string rol = Console.ReadLine();
                auxiliar.Rol = rol.Equals("") ?auxiliar.Rol :rol;

                auxiliar = funciones.Modificar(auxiliar);
                Console.WriteLine(auxiliar != null ? "Se ha modificado con exito" : "No se ha podido modificar esta asignacion.");
            }
            else
            {
                ModificarPorNombre();
            }
        }
        public void ModificarPorNombre()
        {
            AutoresYComics auxiliar = new AutoresYComics();
            Console.WriteLine();
            Console.Write("Introduce el id de la asignacion que quieres modificar: ");
            int idAsignacion = 0;
            int.TryParse(Console.ReadLine(), out idAsignacion);
            auxiliar = funciones.ComprobarExistencia(idAsignacion);

            bool consulta = false;
            string nombreAutor = ".";
            while (!consulta && !nombreAutor.Equals(""))
            {
                Console.Write("Introduce el nombre del autor(intro para no modificar): ");
                nombreAutor = Console.ReadLine();
                if (!nombreAutor.Equals(""))
                {
                    consulta = Contexto.Autores.Any(x => x.Nombre.Equals(nombreAutor));
                }
            }
            auxiliar.AutorId = nombreAutor.Equals("") ? auxiliar.AutorId:Contexto.Autores.Where(x => x.Nombre.Equals(nombreAutor)).First().Id;


            consulta = false;
            string nombreComic = ".";
            while (!consulta && !nombreComic.Equals(""))
            {
                Console.Write("Introduce el nombre del comic(intro para no modificar): ");
                nombreComic = Console.ReadLine();
                if (!nombreComic.Equals(""))
                {
                    consulta = Contexto.Comics.Any(x => x.Titulo.Equals(nombreComic));
                }
            }
            auxiliar.ComicId = nombreComic.Equals("") ? auxiliar.ComicId:Contexto.Comics.Where(x => x.Titulo.Equals(nombreComic)).First().Id;



            Console.WriteLine("Introduce el rol que tiene el autor (intro para no modificar): ");
            string rol = Console.ReadLine();
            auxiliar.Rol = rol.Equals("") ? auxiliar.Rol:rol;

            auxiliar = funciones.Modificar(auxiliar);
            Console.WriteLine(auxiliar != null ? "Se ha modificado con exito" : "No se ha podido modificar esta asignacion.");
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
                Console.WriteLine("Todavia no hay ninguna asignacion añadida.");
            }
        }
    }
}
