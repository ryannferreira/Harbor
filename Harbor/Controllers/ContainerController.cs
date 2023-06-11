using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Harbor.Data;
using Harbor.Models;

namespace Harbor.Controllers
{
    public class ContainerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContainerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Container
        public async Task<IActionResult> Index()
        {
              return _context.Container != null ? 
                          View(await _context.Container.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Container'  is null.");
        }

        // GET: Container/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Container == null)
            {
                return NotFound();
            }

            var containerModel = await _context.Container
                .FirstOrDefaultAsync(m => m.IdContainer == id);
            if (containerModel == null)
            {
                return NotFound();
            }

            return View(containerModel);
        }

        public IActionResult Create()
        {
            var tipo = new List<SelectListItem>
            {
                new SelectListItem {Value= "20", Text ="20"},
                new SelectListItem {Value= "40", Text ="40"}
            };

            ViewBag.Tipo = tipo;

            var status = new List<SelectListItem>
            {
                new SelectListItem {Value= "Cheio", Text ="Cheio"},
                new SelectListItem {Value= "Vazio", Text ="Vazio"}
            };

            ViewBag.Status = status;

            var categoria = new List<SelectListItem>
            {
                new SelectListItem {Value= "Importação", Text ="Importação"},
                new SelectListItem {Value= "Exportação", Text ="Exportação"}
            };

            ViewBag.Categoria = categoria;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContainer,NomeCliente,NumeroContainer,Tipo,Status,Categoria")] ContainerModel containerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(containerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(containerModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var tipo = new List<SelectListItem>
            {
                new SelectListItem {Value= "20", Text ="20"},
                new SelectListItem {Value= "40", Text ="40"}
            };

            ViewBag.Tipo = tipo;

            var status = new List<SelectListItem>
            {
                new SelectListItem {Value= "Cheio", Text ="Cheio"},
                new SelectListItem {Value= "Vazio", Text ="Vazio"}
            };

            ViewBag.Status = status;

            var categoria = new List<SelectListItem>
            {
                new SelectListItem {Value= "Importação", Text ="Importação"},
                new SelectListItem {Value= "Exportação", Text ="Exportação"}
            };

            ViewBag.Categoria = categoria;

            if (id == null || _context.Container == null)
            {
                return NotFound();
            }

            var containerModel = await _context.Container.FindAsync(id);
            if (containerModel == null)
            {
                return NotFound();
            }
            return View(containerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContainer,NomeCliente,NumeroContainer,Tipo,Status,Categoria")] ContainerModel containerModel)
        {
            if (id != containerModel.IdContainer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(containerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContainerModelExists(containerModel.IdContainer))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(containerModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Container == null)
            {
                return NotFound();
            }

            var containerModel = await _context.Container
                .FirstOrDefaultAsync(m => m.IdContainer == id);
            if (containerModel == null)
            {
                return NotFound();
            }

            return View(containerModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Container == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Container'  is null.");
            }
            var containerModel = await _context.Container.FindAsync(id);
            if (containerModel != null)
            {
                _context.Container.Remove(containerModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContainerModelExists(int id)
        {
          return (_context.Container?.Any(e => e.IdContainer == id)).GetValueOrDefault();
        }
    }
}
