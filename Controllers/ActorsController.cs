using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Data;

namespace MinimalAPI.Controllers
{
    public class ActorsController : Controller
    {
        private readonly UserDbContext _context;

        public ActorsController(UserDbContext context)
        {
            _context = context;     
        }

        public IActionResult Index()
        {
            var data = _context.Actors.ToList();

            return View();
        }
    }
}
