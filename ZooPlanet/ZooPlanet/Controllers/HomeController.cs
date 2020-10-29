using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ZooPlanet.Models;
using ZooPlanet.Repositories;

namespace ZooPlanet.Controllers
{
    public class HomeController : Controller
    {
        
        [Route("/")]
        public IActionResult Index()
        {
            using animalesContext ctx = new animalesContext();
            ClasesRepository clasesRepository = new ClasesRepository(ctx);
            return View(clasesRepository.GetAll().ToList());
        }
        [Route("{id}")]
        public IActionResult Clase(string Id)
        {
            animalesContext ctx = new animalesContext();

            ViewBag.Clase = Id;
            EspeciesRepository especiesRepository = new EspeciesRepository(ctx);
            return View(especiesRepository.GetEspeciesByClase(Id));

        }
    }

}