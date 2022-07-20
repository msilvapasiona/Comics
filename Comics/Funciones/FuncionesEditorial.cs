using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comics.Modelos;
using Microsoft.EntityFrameworkCore;
using Comics.Controladores;

namespace Comics.Funciones
{
    internal class FuncionesEditorial : IFunciones
    {
        public FuncionesEditorial()
        {
            this.funciones = new CrudDAO<Editorial>(Contexto,Contexto.Editoriales);
        }

        public FuncionesEditorial(ICrud<Editorial> funciones)
        {
            this.funciones = funciones;
        }

        public Context Contexto { get; } = new Context();
        public ICrud<Editorial> funciones { get; set; }
        public void Añadir()
        {
            Editorial editorial = new Editorial();
            Console.WriteLine();
            Console.Write("Introduce el nombre de la editorial: ");
            editorial.Nombre = Console.ReadLine();
            Console.Write("Introduce el pais de la editorial: ");
            editorial.Pais = Console.ReadLine();

            editorial = funciones.Guardar(editorial);
            Console.WriteLine(editorial != null ? $"Editorial añadida con el id {editorial.Id}" : "No se ha podido guardar esta editorial, puede que ya exista un autor igual.");
        }

        public void Eliminar()
        {
            Console.WriteLine();
            Console.WriteLine("Introduzca el id a eliminar: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);
            Editorial auxiliar = funciones.ComprobarExistencia(id);
            if (auxiliar != null)
            {
                Console.WriteLine($"Se Borrará la editorial {auxiliar.Nombre}. ¿Está seguro?(s/n)");
                string respuesta = Console.ReadLine();
                if (respuesta.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    auxiliar = funciones.Eliminar(auxiliar.Id);
                    Console.WriteLine("Editorial eliminada con exito.");
                }
                else
                {
                    Console.WriteLine("Operacion Cancelada.");
                }
            }
            else
            {
                Console.WriteLine("No existe ninguna editorial con este id.");
            }
        }

        public void Modificar()
        {
            Editorial editorial = new Editorial();
            Console.WriteLine();
            Console.WriteLine("Introduzca el id a modificar: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);
            editorial = funciones.ComprobarExistencia(id);


            Console.WriteLine("Introduzca el nuevo nombre (intro para no modificar): ");
            string nombre = Console.ReadLine();
            editorial.Nombre = nombre.Equals("") ? editorial.Nombre: nombre;
            Console.WriteLine("Introduzca la nueva nacionalidad (intro para no modificar): ");
            string pais = Console.ReadLine();
            editorial.Pais = pais.Equals("") ? editorial.Pais: pais;

            editorial = funciones.Modificar(editorial);
            Console.WriteLine(editorial != null ? "Se ha modificado con exito." : "No se ha podido modificar esta editorial.");
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
    }
}
