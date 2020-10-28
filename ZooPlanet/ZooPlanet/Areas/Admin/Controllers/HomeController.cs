using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ZooPlanet.Models;
using ZooPlanet.Models.ViewModels;
using ZooPlanet.Repositories;

namespace ZooPlanet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {    
        public IActionResult Index()
        {
            animalesContext context = new animalesContext();
            EspeciesRepository repos = new EspeciesRepository(context);

            var listaEspecies = repos.GetAll();

            return View(listaEspecies);
        }

        public IActionResult Agregar()
        {
            animalesContext context = new animalesContext();
            EspeciesViewModel vm = new EspeciesViewModel();
            ClasesRepository clases = new ClasesRepository(context);
            vm.Clases = clases.GetAll();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(EspeciesViewModel vm)
        {
            animalesContext context = new animalesContext();

            try
            {
                EspeciesRepository repos = new EspeciesRepository(context);
                repos.Insert(vm.Especie);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ClasesRepository clases = new ClasesRepository(context);
                vm.Clases = clases.GetAll();
                return View(vm);
            }
        }

        public IActionResult Editar(int id)
        {
            animalesContext context = new animalesContext();
            EspeciesViewModel vm = new EspeciesViewModel();
            EspeciesRepository EspeciesRepos = new EspeciesRepository(context);

            vm.Especie = EspeciesRepos.GetById(id);
            if(vm.Especie==null)
            {
                return RedirectToAction("Index");
            }
            ClasesRepository ClasesRepos = new ClasesRepository(context);
            vm.Clases = ClasesRepos.GetAll();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Editar(EspeciesViewModel vm)
        {
            animalesContext context = new animalesContext();

            try
            {
                EspeciesRepository EspeciesRepos = new EspeciesRepository(context);
                var esp = EspeciesRepos.GetById(vm.Especie.Id);
                if(esp!=null)
                {
                    esp.Especie = vm.Especie.Especie;
                    esp.Habitat = vm.Especie.Habitat;
                    esp.IdClase = vm.Especie.IdClase;
                    esp.Observaciones = vm.Especie.Observaciones;
                    esp.Peso = vm.Especie.Peso;
                    esp.Tamaño = vm.Especie.Tamaño;

                    EspeciesRepos.Update(esp);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ClasesRepository clases = new ClasesRepository(context);
                vm.Clases = clases.GetAll();
                return View(vm);
            }
        }

        public IActionResult Eliminar(int id)
        {
            using (animalesContext context = new animalesContext())
            {
                EspeciesRepository especiesRepos = new EspeciesRepository(context);
                var esp = especiesRepos.GetById(id);

                if(esp!=null)
                {
                    return View(esp);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
                
        }

        [HttpPost]
        public IActionResult Eliminar(Especies e)
        {
            using(animalesContext context=new animalesContext())
            {
                EspeciesRepository repos = new EspeciesRepository(context);
                var esp = repos.GetById(e.Id);

                if(esp!=null)
                {
                    repos.Delete(esp);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "La especie no existe o ya ha sido eliminada");
                    return View(e);
                }
            }
        }

        

        public IActionResult Imagen(int id)
        {
            return View();
        }
    }
}
