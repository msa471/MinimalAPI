using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data;

namespace MinimalAPI.Controllers
{
    public class ProducersController : Controller
    {
        private readonly UserDbContext _context;

        public ProducersController(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allProducers  = await _context.Producers.ToListAsync();    
            return View();
        }
    }
}
