using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.DataLinkLayer;

namespace MinimalAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserDbContext _context; 

        public UsersController(UserDbContext context)
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
