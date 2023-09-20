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

        public void NotaFiscaisController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
