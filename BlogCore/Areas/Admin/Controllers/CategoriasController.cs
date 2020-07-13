using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria cat)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(cat);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(cat);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria cat = new Categoria();
            cat = _contenedorTrabajo.Categoria.Get(id);

            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria cat)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Update(cat);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(cat);
        }

        #region LLAMADAS A LA API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll()});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Categoria.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando categoría" });
            }
            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Categoría borrada correctamente" });
        }
        #endregion
    }
}
