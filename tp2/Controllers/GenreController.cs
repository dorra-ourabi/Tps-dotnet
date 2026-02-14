using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp2.Models;

namespace tp2.Controllers
{[ApiController]
    [Route("Genre")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Injection du DbContext
        public GenreController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var genres = _context.genres.ToList();
            return View(genres); // strongly-typed view showing all genres
        }
        // GET: /Genre/Index/1
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Genre/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken] // <-- validation anti-XSS / CSRF
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Index"); // retour à la liste
            }

            return View(genre); // si la validation échoue
        }

    }
}