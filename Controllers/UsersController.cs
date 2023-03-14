using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.DataLinkLayer;

namespace MinimalAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context; 

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

            public async Task<IActionResult> Index()
        {
            var allUsers = await _context.Users.ToListAsync();
            return View(allUsers);
        }
    }
}
