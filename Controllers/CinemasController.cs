using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data;

namespace MinimalAPI.Controllers
{
    public class CinemasController : Controller
    {
        private readonly UserDbContext _context;

        public CinemasController(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allProducers = await _context.Cinemas.ToListAsync();
            return View();
        }
    }
}
