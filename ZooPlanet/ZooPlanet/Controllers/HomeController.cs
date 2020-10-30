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
        animalesContext context;
        public HomeController(animalesContext ctx)
        {
            context = ctx;
        }
        [Route("/")]
        public IActionResult Index()
        {
            ClasesRepository clasesRepository = new ClasesRepository(context);
            return View(clasesRepository.GetAll().ToList());
        }
        [Route("{Id}")]
        public IActionResult Clase(string Id)
        {

            ViewBag.Clase = Id;
            EspeciesRepository especiesRepository = new EspeciesRepository(context);
            return View(especiesRepository.GetEspeciesByClase(Id));

        }
    }

}