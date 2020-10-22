using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actividad1.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Actividad1.Repositories
{
    public class Reporitory<T> where T : class
    {
        public sistem14_mapacurricularContext Context { get; set; }

        public Reporitory(sistem14_mapacurricularContext context)
        {
            Context = context;
        }

        //Patron de repositorios
        //metodos que todo repositorio debe tener

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public T Get(object id)
        {
            return Context.Find<T>(id);
        }
        public void Insert(T entidad)
        {
            Context.Add<T>(entidad);
            Context.SaveChanges();
        }
        public void Delete(T entidad)
        {
            Context.Remove<T>(entidad);
            Context.SaveChanges();
        }
        public void Update(T entidad)
        {
            Context.Update<T>(entidad);
            Context.SaveChanges();
        }

    }
}
