using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MinimalAPI.Controllers
{
    public class MoviesController : Controller
    {

        private readonly UserDbContext _context;

        public MoviesController(UserDbContext context)
        {
            _context = context; 
        }

       public async Task<IActionResult> Index()
        {
            var allProducers= await _context.Movies.ToListAsync();
            return View();
        }
    }
}
