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
    public class MovimentacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovimentacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movimentacoes.Include(m => m.Container);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movimentacoes == null)
            {
                return NotFound();
            }

            var movimentacoesModel = await _context.Movimentacoes
                .Include(m => m.Container)
                .FirstOrDefaultAsync(m => m.IdMovimentacao == id);
            if (movimentacoesModel == null)
            {
                return NotFound();
            }

            return View(movimentacoesModel);
        }

        public IActionResult Relatorio()
        {
            var relatorioMov = _context.Movimentacoes
                .GroupBy(m => new { m.NomeCliente, m.TipoMovimentacao })
                .Select(g => new
                {
                    Cliente = g.Key.NomeCliente,
                    TipoMovimentacao = g.Key.TipoMovimentacao,
                    Total = g.Count()
                })
                .ToList();

            var relatorioCont = _context.Container
                .GroupBy(c => c.Categoria)
                .Select(g => new
                {
                    Categoria = g.Key,
                    Total = g.Count()
                })
                .ToList();

            int totalImportacao = relatorioCont.Where(r => r.Categoria == "Importação").Sum(r => r.Total);
            int totalExportacao = relatorioCont.Where(r => r.Categoria == "Exportação").Sum(r => r.Total);

            ViewBag.RelatorioMov = relatorioMov;
            ViewBag.RelatorioCont = relatorioCont;
            ViewBag.TotalImportacao = totalImportacao;
            ViewBag.TotalExportacao = totalExportacao;

            return View();
        }

        public IActionResult Create()
        {
            ViewData["IdContainer"] = new SelectList(_context.Container, "IdContainer", "IdContainer");

            var containers = _context.Container.ToList();

            ViewBag.ContainerInfo = containers.Select(c => new
            {
                c.IdContainer,
                c.NumeroContainer,
                c.NomeCliente
            });
            
            
            var tipoMovimentacoes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Embarque", Text= "Embarque"},
                new SelectListItem { Value = "Descarga", Text= "Descarga"},
                new SelectListItem { Value = "Gate in", Text= "Gate in"},
                new SelectListItem { Value = "Gate out", Text= "Gate out"},
                new SelectListItem { Value = "Reposicionamento", Text= "Reposicionamento"},
                new SelectListItem { Value = "Pesagem", Text= "Pesagem"},
                new SelectListItem { Value = "Scanner", Text= "Scanner"},
            };

            ViewBag.TipoMovimentacoes = tipoMovimentacoes;
            
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMovimentacao,IdContainer,NomeCliente,NumeroContainer,TipoMovimentacao,DataInicio,DataTermino")] MovimentacoesModel movimentacoesModel)
        {
            //if (ModelState.IsValid)
            //{
                
            //}

            _context.Add(movimentacoesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //ViewData["IdContainer"] = new SelectList(_context.Container, "IdContainer", "IdContainer", movimentacoesModel.IdContainer);

            //return View(movimentacoesModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var tipoMovimentacoes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Embarque", Text= "Embarque"},
                new SelectListItem { Value = "Descarga", Text= "Descarga"},
                new SelectListItem { Value = "Gate in", Text= "Gate in"},
                new SelectListItem { Value = "Gate out", Text= "Gate out"},
                new SelectListItem { Value = "Reposicionamento", Text= "Reposicionamento"},
                new SelectListItem { Value = "Pesagem", Text= "Pesagem"},
                new SelectListItem { Value = "Scanner", Text= "Scanner"},
            };

            ViewBag.TipoMovimentacoes = tipoMovimentacoes;

            if (id == null || _context.Movimentacoes == null)
            {
                return NotFound();
            }

            var movimentacoesModel = await _context.Movimentacoes.FindAsync(id);
            if (movimentacoesModel == null)
            {
                return NotFound();
            }

            ViewData["IdContainer"] = new SelectList(_context.Container, "IdContainer", "IdContainer");

            return View(movimentacoesModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMovimentacao,IdContainer,NomeCliente,NumeroContainer,TipoMovimentacao,DataInicio,DataTermino")] MovimentacoesModel movimentacoesModel)
        {
            if (id != movimentacoesModel.IdMovimentacao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacoesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacoesModelExists(movimentacoesModel.IdMovimentacao))
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
            ViewData["IdContainer"] = new SelectList(_context.Container, "IdContainer", "Categoria", movimentacoesModel.IdContainer);
            return View(movimentacoesModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movimentacoes == null)
            {
                return NotFound();
            }

            var movimentacoesModel = await _context.Movimentacoes
                .Include(m => m.Container)
                .FirstOrDefaultAsync(m => m.IdMovimentacao == id);
            if (movimentacoesModel == null)
            {
                return NotFound();
            }

            return View(movimentacoesModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movimentacoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movimentacoes'  is null.");
            }
            var movimentacoesModel = await _context.Movimentacoes.FindAsync(id);
            if (movimentacoesModel != null)
            {
                _context.Movimentacoes.Remove(movimentacoesModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacoesModelExists(int id)
        {
          return (_context.Movimentacoes?.Any(e => e.IdMovimentacao == id)).GetValueOrDefault();
        }
    }
}
