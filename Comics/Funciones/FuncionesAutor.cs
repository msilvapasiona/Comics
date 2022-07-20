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
    public class FuncionesAutor:IFunciones
    {
        public FuncionesAutor()
        {
            this.funciones = new CrudDAO<Autor>(Contexto, Contexto.Autores);
        }
        public FuncionesAutor(ICrud<Autor> funciones)
        {
            this.funciones = funciones;
        }

        public Context Contexto { get; } = new Context();
        public ICrud<Autor> funciones { get; set; } 

        public void Añadir()
        {
            Autor autor = new Autor();
            Console.WriteLine();
            Console.Write("Introduce el nombre del autor: ");
            autor.Nombre = Console.ReadLine();
            Console.Write("Introduce la nacionalidad del autor: ");
            autor.Nacionalidad = Console.ReadLine();
            Console.Write("Introduce el año de nacimiento del autor: ");
            int añoNacimiento = 0;
            int.TryParse(Console.ReadLine(), out añoNacimiento);
            autor.AñoNacimiento = añoNacimiento;
            autor = funciones.Guardar(autor);
            Console.WriteLine(autor != null ? $"Autor añadido con el id {autor.Id}" : "No se ha podido guardar este autor, puede que ya exista un autor igual.");
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
                Console.WriteLine("Todavia no hay ningun autor añadido.");
            }
        }
        public void Modificar()
        {
            Autor auxiliar = new Autor();

            Console.WriteLine();
            Console.WriteLine("Introduzca el id a modificar: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);
            auxiliar = funciones.ComprobarExistencia(id);


            Console.WriteLine("Introduzca el nuevo nombre (intro para no modificar): ");
            string nombre = Console.ReadLine();
            auxiliar.Nombre = nombre.Equals("") ? auxiliar.Nombre:nombre;

            Console.WriteLine("Introduzca la nueva nacionalidad (intro para no modificar): ");
            string nacionalidad = Console.ReadLine();
            auxiliar.Nacionalidad = nacionalidad.Equals("") ? auxiliar.Nacionalidad:nacionalidad;

            Console.WriteLine("Introduce el año de nacimiento del autor (-1 para no modificar):");
            int añoNacimiento = 0;
            int.TryParse(Console.ReadLine(), out añoNacimiento);
            auxiliar.AñoNacimiento = añoNacimiento == -1 ? auxiliar.AñoNacimiento:añoNacimiento;

            auxiliar = funciones.Modificar(auxiliar);
            Console.WriteLine(auxiliar != null ? "Se ha modificado con exito." : "No se ha podido modificar este autor.");
        }
        public void Eliminar()
        {
            Console.WriteLine();
            Console.WriteLine("Introduzca el id a eliminar: ");
            int id = 0;
            int.TryParse(Console.ReadLine(), out id);
            Autor auxiliar = funciones.ComprobarExistencia(id);
            if (auxiliar != null)
            {
                Console.WriteLine($"Se Borrará el autor {auxiliar.Nombre}. ¿Está seguro?(s/n)");
                string respuesta = Console.ReadLine();
                if (respuesta.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    auxiliar = funciones.Eliminar(auxiliar.Id);
                    Console.WriteLine("Autor eliminado con exito.");
                }
                else
                {
                    Console.WriteLine("Operacion Cancelada.");
                }
            }
            else
            {
                Console.WriteLine("No existe ningun autor con este id.");
            }
        }
    }
}
