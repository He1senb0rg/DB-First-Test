using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using db_ado_ex.Models;

namespace db_ado_ex.Controllers
{
    public class Artigos : Controller
    {

        private readonly IArtigoDAL dal;

        public Artigos(IArtigoDAL artigo)
        {
            dal = artigo;
        }
        public IActionResult Index()
        {
            List<Artigo> listaArtigos = new List<Artigo>();
            listaArtigos = dal.GetAllArtigos().ToList();
            return View(listaArtigos);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Artigo artigo = dal.GetArtigo(id);
            if (artigo == null)
            {
                return NotFound();
            }
            return View(artigo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Artigo artigo)
        {
            if (ModelState.IsValid)
            {
                dal.AddArtigo(artigo);
                return RedirectToAction("Index");
            }
            return View(artigo);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Artigo artigo = dal.GetArtigo(id);
            if (artigo == null)
            {
                return NotFound();
            }
            return View(artigo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Artigo artigo)
        {
            if (id != artigo.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                dal.UpdateArtigo(artigo);
                return RedirectToAction("Index");
            }
            return View(artigo);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Artigo artigo = dal.GetArtigo(id);
            if (artigo == null)
            {
                return NotFound();
            }
            return View(artigo);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            dal.DeleteArtigo(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
