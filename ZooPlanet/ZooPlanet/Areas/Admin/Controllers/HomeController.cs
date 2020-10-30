using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        public IWebHostEnvironment Environment { get; set; }
        animalesContext context;
        public HomeController(IWebHostEnvironment env, animalesContext ctx)
        {
            Environment = env;
            context = ctx;
        }
        public IActionResult Index()
        {

            EspeciesRepository repos = new EspeciesRepository(context);

            var listaEspecies = repos.GetAll();

            return View(listaEspecies);
        }
        
        public IActionResult Agregar()
        {
            EspeciesViewModel vm = new EspeciesViewModel();
            ClasesRepository clases = new ClasesRepository(context);
            vm.Clases = clases.GetAll();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(EspeciesViewModel vm)
        {

            try
            {
                EspeciesRepository repos = new EspeciesRepository(context);
                repos.Insert(vm.Especie);

                return RedirectToAction("Index","Home");
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
            EspeciesViewModel vm = new EspeciesViewModel();
            EspeciesRepository EspeciesRepos = new EspeciesRepository(context);

            vm.Especie = EspeciesRepos.GetById(id);
            if (vm.Especie == null)
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

            try
            {
                EspeciesRepository EspeciesRepos = new EspeciesRepository(context);
                var esp = EspeciesRepos.GetById(vm.Especie.Id);
                if (esp != null)
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

            EspeciesRepository especiesRepos = new EspeciesRepository(context);
            var esp = especiesRepos.GetById(id);

            if (esp != null)
            {
                return View(esp);
            }
            else
            {
                return RedirectToAction("Index");
            }


        }

        [HttpPost]
        public IActionResult Eliminar(Especies e)
        {

            EspeciesRepository repos = new EspeciesRepository(context);
            var esp = repos.GetById(e.Id);

            if (esp != null)
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


        public IActionResult Imagen(int id)
        {
            EspeciesViewModel vm = new EspeciesViewModel();
            EspeciesRepository repos = new EspeciesRepository(context);

            vm.Especie = repos.GetById(id);
            if (vm.Especie == null)
            {
                return RedirectToAction("Index");
            }
            if (System.IO.File.Exists(Environment.WebRootPath + $"/especies/{vm.Especie.Id}.jpg"))
            {
                vm.Imagen = $"{vm.Especie.Id}.jpg";
            }
            else
            {
                vm.Imagen = "nophoto.jpg";
            }
            return View(vm);
        }

        [HttpPost]
        public IActionResult Imagen(EspeciesViewModel vm)
        {
            try
            {
                if (vm.Archivo == null)
                {
                    ModelState.AddModelError("", "Debe seleccionar una imagen");
                    return View(vm);

                }
                else
                {
                    if (vm.Archivo.ContentType != "image/jpeg" || vm.Archivo.Length > 1024 * 1024 * 2)
                    {
                        ModelState.AddModelError("", "Debe seleccionar un archivo jpeg menor a 2mb.");
                        return View(vm);
                    }
                    FileStream fs = new FileStream(Environment.WebRootPath + $"/especies/{vm.Especie.Id}.jpg", FileMode.Create);
                    vm.Archivo.CopyTo(fs);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }
    }
}
