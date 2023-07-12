using ListaLibrosMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaLibrosMVC.Controllers
{
    public class LibrosController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Libro Libro { get; set; }
        
        public LibrosController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Libro = new Libro();
            if(id == null) // si no hay id
            {
                return View(Libro); // se crea uno nuevo
            }
            Libro = _db.Libros.FirstOrDefault(u=>u.Id==id); // si no se edita el que tenga el id devuelto
            if(Libro == null)
            {
                return NotFound();
            }
            return View(Libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if(Libro.Id == 0)
                {
                    _db.Libros.Add(Libro);
                }
                else
                {
                    _db.Libros.Update(Libro);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Libro);
        }

        #region APICalls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Libros.ToListAsync() }); // recupera el libro y lo pasa cada vez que se llame esta API
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var libroBD = await _db.Libros.FirstOrDefaultAsync(u => u.Id == id);
            if (libroBD == null)
            {
                return Json(new { success = false, message = "Error al eliminar." });
            }
            _db.Libros.Remove(libroBD);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Eliminado." });
        }
        #endregion
    }
}
