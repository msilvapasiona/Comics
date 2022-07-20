using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Comics.Modelos;
using Comics;
using System.Reflection;

namespace Comics.Controladores
{
    public class CrudDAO<T> : ICrud<T> where T : class, IentidadBD
    {

        public DbContext basedeDatos;

        public CrudDAO(DbContext basedaDatos, DbSet<T> miTabla)
        {
            this.miTabla = miTabla;
            this.basedeDatos = basedaDatos;
        }

        public DbSet<T> miTabla { get; set; }
        public T? ComprobarExistencia(T elemento)
        {
            if (miTabla.Where(x => x.Equals(elemento)).ToList().Count() > 0)
            {
                T auxiliar = miTabla.Where(x => x.Equals(elemento)).First();
            }
            return default(T);
        }

        public T? ComprobarExistencia(int id)
        {
            if (miTabla.Where(x => x.Id == id ).ToList().Count() > 0)
            {
                T auxiliar = miTabla.Where(x => x.Id == id).First();
                return auxiliar;
            }
            return default(T);
        }

        public T? Eliminar(int id)
        {
            T elemento = ComprobarExistencia(id);
            T auxiliar = elemento;
            if (elemento != null)
            { 
                basedeDatos.Remove(elemento);
                basedeDatos.SaveChanges();
                return auxiliar;
            }
            return default(T);
        }

        public T? Guardar(T elemento)
        {
            if (ComprobarExistencia(elemento) == null)
            {
                basedeDatos.Add(elemento);
                basedeDatos.SaveChanges();
                return elemento;
            }
            return default(T);
        }

        public T? Modificar(T elemento)
        {
            T auxiliar = ComprobarExistencia(elemento.Id);
            auxiliar = elemento;
            basedeDatos.SaveChanges();
            return auxiliar;
        }

        public virtual IList<T> Mostrar()
        {
            return miTabla.OrderBy(x => x.Id).ToList();
        }
    }
    public class ComicDAO<T> : CrudDAO<T> where T : Comic
    {
        public ComicDAO(DbContext basedaDatos, DbSet<T> miTabla) : base(basedaDatos, miTabla)
        {
        }
        public override IList<T> Mostrar()
        {
            return miTabla.OrderBy(x => x.Id).Include(x => x.Categoria).Include(x => x.Editorial).ToList();
        }
    }
    public class AutorComicDAO<T> : CrudDAO<T> where T : AutoresYComics
    {
        public AutorComicDAO(DbContext basedaDatos, DbSet<T> miTabla) : base(basedaDatos, miTabla)
        {
        }
        public override IList<T> Mostrar()
        {
            return miTabla.OrderBy(x => x.Id).Include(x => x.Autor).Include(x => x.Comic).ToList();
        }
    }
}
