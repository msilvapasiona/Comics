using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comics.Modelos
{
    public class Editorial :IentidadBD
    {
        public Editorial()
        {
        }

        public Editorial(string nombre, string pais)
        {
            Nombre = nombre;
            Pais = pais;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public IList<Comic> ComicsDeLaEditorial { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is Editorial editorial &&
                   Nombre == editorial.Nombre &&
                   Pais == editorial.Pais;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre, Pais);
        }
        public override string ToString()
        {
            return "Id: " + Id + ", nombre: " + Nombre + ", pais: " + Pais + ".";
        }
    }
}
