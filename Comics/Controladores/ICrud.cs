using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comics.Controladores
{
    public interface ICrud<T>
    {
        public T? Guardar(T elemento);
        public IList<T> Mostrar();
        public T? Eliminar(int id);
        public T? Modificar(T elemento);
        public T? ComprobarExistencia(T elemento);
        public T? ComprobarExistencia(int id);
    }
}
