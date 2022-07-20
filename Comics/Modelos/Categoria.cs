using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comics.Controladores;
using Comics.Funciones;

namespace Comics.Modelos
{

    public class Categoria : IentidadBD
    {

        public Categoria()
        {
        }

        public Categoria(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public IList<Comic> Comics_Categoria { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Categoria categoria &&
                   Nombre == categoria.Nombre &&
                   Descripcion == categoria.Descripcion;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre, Descripcion);
        }

        public override string ToString()
        {
            return "Id: " + Id + ",nombre: " + Nombre + ", descripcion: " + Descripcion + ".";
        }
    }
}
