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
    }
}
