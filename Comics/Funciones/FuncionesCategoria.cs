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
    public class FuncionesCategoria: IFunciones
    {
        public Context Contexto { get; } = new Context();

        public ICrud<Categoria> funciones { get; set; } 

        public FuncionesCategoria()
        {
            this.funciones = new CrudDAO<Categoria>(Contexto, Contexto.Categorias);
        }
        public FuncionesCategoria(ICrud<Categoria> funciones)
        {
            this.funciones = funciones;
        }

        public void Añadir()
        {
            Categoria categoria = new Categoria();
            Console.WriteLine();
            Console.Write("Introduce el nombre de la categoria: ");
            categoria.Nombre = Console.ReadLine();
            Console.Write("Introduce la descripcion de la categoria :");
            categoria.Descripcion = Console.ReadLine();
            categoria = funciones.Guardar(categoria);
            Console.WriteLine(categoria != null ? $"Categoria creada con el id {categoria.Id}" : "No se ha podido guardar esta categoría, puede que ya exista una categoria igual.");
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
                Console.WriteLine("Todavia no hay ninguna categoria añadida.");
            }
        }
        public void Modificar()
        {
            Categoria auxiliar = new Categoria();

            Console.WriteLine();
            Console.WriteLine("Introduzca el id a modificar: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);
            auxiliar = funciones.ComprobarExistencia(id);

            Console.WriteLine("Introduzca el nuevo nombre (intro para no modificar): ");
            string nombre = Console.ReadLine();
            auxiliar.Nombre = nombre.Equals("") ? auxiliar.Nombre: nombre;

            Console.WriteLine("Introduzca la nueva descripcion (intro para no modificar): ");
            string descripcion = Console.ReadLine();
            auxiliar.Descripcion = descripcion.Equals("") ? auxiliar.Descripcion:descripcion;

            auxiliar = funciones.Modificar(auxiliar);
            Console.WriteLine(auxiliar != null ? "Se ha modificado con exito" : "No se ha podido modificar esta categoría.");
        }
        public void Eliminar()
        {
            Console.WriteLine();
            Console.WriteLine("Introduzca el id a eliminar: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);
            Categoria auxiliar = funciones.ComprobarExistencia(id);
            if (auxiliar != null)
            {
                Console.WriteLine($"Se Borrará la categoría {auxiliar.Nombre}. ¿Está seguro?(s/n)");
                string respuesta = Console.ReadLine();
                if (respuesta.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    auxiliar = funciones.Eliminar(auxiliar.Id);
                    Console.WriteLine("Categoria eliminada con exito.");
                }
                else
                {
                    Console.WriteLine("Operacion Cancelada.");
                }
            }
            else
            {
                Console.WriteLine("No existe ninguna categoria con este id.");
            }
        }
    }
}
