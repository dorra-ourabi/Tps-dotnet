using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp2.Models;

namespace tp2.Controllers
{
    [Route("Movie")]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Injection du DbContext
        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Index")]
        public IActionResult Index(int pageNumber = 1)
        {
            int pageSize = 5; // Nombre de films par page

            var movies = _context.movies
                .OrderBy(m => m.Name); // Tri alphabétique

            // Pagination
            var pagedMovies = movies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Pour afficher la pagination dans la vue
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)_context.movies.Count() / pageSize);

            return View(pagedMovies);
        }
        // GET: /Genre/Index/1
        [HttpGet("Edit")]
        public IActionResult Edit()
        {
            return View();
        }

        // POST: /Genre/Create
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken] // <-- validation anti-XSS / CSRF
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Index"); // retour à la liste
            }

            return View(movie); // si la validation échoue
        }
        // GET: /Movie/Delete/5
        [HttpGet("Delete/{name}")]
        public IActionResult Delete(string name)
        {
            var movie = _context.movies.FirstOrDefault(m => m.Name == name);
            if (movie == null)
                return NotFound(); // 404 if movie doesn't exist

            return View(movie); // show a confirmation page
        }
        

    }
}