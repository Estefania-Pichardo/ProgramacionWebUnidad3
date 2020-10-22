using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Actividad1.Models;
using Actividad1.Repositories;

namespace Actividad1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Agregar()
        {
            //Carreras c = new Carreras()
            //{
            //    Clave = "Z",
            //    Nombre = "Carrera de prueba",
            //    Especialidad = "Nombre de la especialidad",
            //    Plan = "ABC123",

            //};
            
            Materias m = new Materias()
            {
                Clave="ABC123",
                Creditos=5,
                HorasPracticas=3,
                HorasTeoricas=2,
                IdCarrera=1,
                Nombre="materia de prueba",
                Semestre=1
            };
            sistem14_mapacurricularContext context = new sistem14_mapacurricularContext();

            Reporitory<Materias> repos = new Reporitory<Materias>(context);
            repos.Insert(m);

            return Ok("La carrera se agrego");
        }
    }
}
