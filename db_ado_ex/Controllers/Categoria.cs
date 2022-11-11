using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using db_ado_ex.Models;

namespace db_ado_ex.Controllers
{
    public class Categoria : Controller
    {

        private readonly ICategoriaDAL dal;

        public Categoria(ICategoriaDAL categoria)
        {
            dal = categoria;
        }
        public IActionResult Index()
        {
            List<Models.Categoria> listaCategorias = new List<Models.Categoria>();
            listaCategorias = dal.GetAllCategorias().ToList();
            return View(listaCategorias);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Categoria categoria = dal.GetCategoria(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Models.Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                dal.AddCategoria(categoria);
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Categoria categoria = dal.GetCategoria(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Models.Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                dal.UpdateCategoria(categoria);
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Categoria categoria = dal.GetCategoria(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            dal.DeleteCategoria(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
