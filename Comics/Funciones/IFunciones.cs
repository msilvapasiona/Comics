using Comics.Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comics.Modelos;
namespace Comics.Funciones
{
    public interface IFunciones
    {
        public Context Contexto { get;}
        public void Añadir();
        public void Mostrar();
        public void Modificar();
        public void Eliminar();
    }
}
