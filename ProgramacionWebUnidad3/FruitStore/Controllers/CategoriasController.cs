using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FruitStore.Models;
using FruitStore.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace FruitStore.Controllers
{
    public class CategoriasController : Controller
    {
        [Route("Categorias")]
        public IActionResult Index()
        {
            fruteriashopContext context = new fruteriashopContext();
            Repositories.Repository<Categorias> repos = new Repositories.Repository<Categorias>(context);

            return View(repos.GetAll().Where(x=>x.Eliminado==0).OrderBy(x=>x.Nombre));
        }

        [HttpGet]
        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Categorias c)
        {
            c.Eliminado = 0;

            try
            {
                fruteriashopContext context = new fruteriashopContext();
                CategoriasRepository repos = new CategoriasRepository(context);
                repos.Insert(c);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(c);
            }

        }
        public IActionResult Editar(int id)
        {
            using (fruteriashopContext context = new fruteriashopContext())
            {
                CategoriasRepository repos = new CategoriasRepository(context);

                var categoria = repos.Get(id);

                if(categoria==null)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(categoria);
                }
            }

        }

        [HttpPost]
        public IActionResult Editar(Categorias nuevo)
        {
            try
            {
                using (fruteriashopContext context = new fruteriashopContext())
                {
                    CategoriasRepository repos = new CategoriasRepository(context);

                    var original = repos.Get(nuevo.Id);
                    
                    if (original != null)
                    {
                        original.Nombre = nuevo.Nombre;
                        repos.Update(original);
                    }

                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(nuevo);
            }         
        }

        public IActionResult Eliminar(int id)
        {
            using(fruteriashopContext context=new fruteriashopContext())
            {
                CategoriasRepository repos = new CategoriasRepository(context);
                var categoria= repos.Get(id);

                if(categoria==null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(categoria);
                }

            }

        }

        [HttpPost]
        public IActionResult Eliminar(Categorias c)
        {
            try
            {              
                using (fruteriashopContext context = new fruteriashopContext())
                {
                    //Eliminado fisico
                    //CategoriasRepository repos = new CategoriasRepository(context);
                    //var categoria = repos.Get(c.Id);
                    //repos.Delete(categoria);
                    //return RedirectToAction("Index");


                    //Eliminado Logico
                    CategoriasRepository repos = new CategoriasRepository(context);
                    var categoria = repos.Get(c.Id);
                    categoria.Eliminado = 1;
                    repos.Update(categoria);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(c);
            }

        }
    }
}