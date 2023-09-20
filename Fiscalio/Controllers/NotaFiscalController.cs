using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiscalio.Models; 
using Fiscalio.Persistence; 
using System.Linq;

namespace Fiscalio.Controllers
{
    public class NotaFiscalController : Controller
    {
        private readonly AppDbContext _context;

        public NotaFiscalController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var notaFiscais = _context.NotaFiscais.Include(nf => nf.Itens).ToList();

            var notaFiscalComValorTotal = notaFiscais.Select(nf => new NotaFiscalViewModel
            {
                NotaFiscal = nf,
                ValorTotal = nf.Itens.Sum(item => item.Valor)
            }).ToList();

            return View(notaFiscalComValorTotal);
        }

        
        public IActionResult Create()
        {
            

            return View();
        }
        [HttpPost] 
        public IActionResult Store(NotaFiscal notaFiscal)
        {
            if (ModelState.IsValid)
            {
                

                
                _context.NotaFiscais.Add(notaFiscal);
                _context.SaveChanges();

                
                return RedirectToAction("Index");
            }

            
            return View("Create", notaFiscal);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, NotaFiscal notaFiscal)
        {
            if (id != notaFiscal.IdNota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(notaFiscal);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaFiscalExists(notaFiscal.IdNota))
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
            return View(notaFiscal);
        }

        public IActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscal = _context.NotaFiscais.SingleOrDefault(nf => nf.IdNota == id);

            if (notaFiscal == null)
            {
                return NotFound();
            }

            _context.NotaFiscais.Remove(notaFiscal);
            _context.SaveChanges();

            
            return RedirectToAction(nameof(Index));
        }


        private bool NotaFiscalExists(int id)
        {
            return _context.NotaFiscais.Any(nf => nf.IdNota == id);
        }
    }
}
