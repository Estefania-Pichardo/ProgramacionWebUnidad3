using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZooPlanet.Models;

namespace ZooPlanet.Repositories
{
	public class EspeciesRepository : Repository<Especies>
	{

        public EspeciesRepository(animalesContext ctx):base(ctx)
        {

        }


		public override IEnumerable<Especies> GetAll()
		{
			return base.GetAll().OrderBy(x=>x.Especie);
		}

		public IEnumerable<Especies> GetEspeciesByClase(string Id)
		{
			return Context.Especies
				.Include(x => x.IdClaseNavigation)
				.Where(x => x.IdClaseNavigation.Nombre == Id)
				.OrderBy(x => x.Especie);
		}
        public override Especies GetById(object id)
        {
		
            return Context.Especies.Include(x=>x.IdClaseNavigation).FirstOrDefault(x=>x.Id==(int)id);
        }

        public override bool Validate(Especies e)
        {
			if(string.IsNullOrWhiteSpace(e.Especie))
            {
				throw new Exception("Debe indicar el nombre de la especie");
            }
            if (string.IsNullOrWhiteSpace(e.Habitat))
            {
				throw new Exception("Debe indicar el habitad de la especie");
			}
            if (e.Tamaño == null || e.Tamaño <= 0)
            {
				throw new Exception("Debe indicar el tamaño de la especie");
			}
            if (e.Peso == null || e.Peso <= 0)
            {
				throw new Exception("Debe indicar el peso de la especie");
			}
            if (string.IsNullOrWhiteSpace(e.Observaciones))
            {
				throw new Exception("Debe indicar las observaciones de la especie");
			}
			if(Context.Especies.Any(x=>x.Especie==e.Especie&& x.Id != e.Id))
            {
				throw new Exception("La especie que desea agregar ya esta registrada");
			}
			return true;

        }


	}
}
