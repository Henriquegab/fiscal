using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiscalio.Models; // Substitua pelo namespace correto do seu modelo
using Fiscalio.Persistence; // Substitua pelo namespace correto do seu contexto de banco de dados
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

        // GET: NotaFiscal/Editar/5
        public IActionResult Editar(int? id)
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

            // Aqui você pode passar a nota fiscal para a view de edição
            return View(notaFiscal);
        }

        // POST: NotaFiscal/Editar/5
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
                    // Aqui você pode atualizar a nota fiscal no banco de dados
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

            // Após a deleção, redirecione para a lista de notas fiscais
            return RedirectToAction(nameof(Index));
        }


        private bool NotaFiscalExists(int id)
        {
            return _context.NotaFiscais.Any(nf => nf.IdNota == id);
        }
    }
}
