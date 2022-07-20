using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comics.Controladores;
using Comics.Funciones;

namespace Comics.Modelos
{
    public class Autor : IentidadBD
    {
        public Autor()
        {
        }

        public Autor(string nombre, string nacionalidad, int añoNacimiento)
        {
            Nombre = nombre;
            Nacionalidad = nacionalidad;
            AñoNacimiento = añoNacimiento;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Nacionalidad { get; set; }
        public int AñoNacimiento { get; set; }
        public IList<AutoresYComics> Comics { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Autor autor &&
                   Nombre == autor.Nombre &&
                   Nacionalidad == autor.Nacionalidad &&
                   AñoNacimiento == autor.AñoNacimiento;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre, Nacionalidad, AñoNacimiento);
        }

        public override string ToString()
        {
            return "Id: " + Id + ",nombre: " + Nombre + ", nacionalidad: " + Nacionalidad + ", año de nacimiento: " + AñoNacimiento + ".";
        }
    }
}
