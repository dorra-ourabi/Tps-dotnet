using Microsoft.AspNetCore.Mvc;
using TP3.Models;
using Microsoft.EntityFrameworkCore;
namespace TP3.Controllers
{
    public class CustomersController : Controller

    {
        private readonly ApplicationDbContext _context;

        // Injection du DbContext
        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var clients = _context.customers
                .Select(c => new
                {   c.Id,
                    c.Name,
                    c.membershiptypes.DiscountRate
                })
                .ToList();

            return View(clients);
        }

        [HttpGet] // Optionnel mais recommandé
        public IActionResult Add()
        {
            return View();
        }

        // --- 2. CETTE MÉTHODE ENREGISTRE LES DONNÉES ---
        [HttpPost]
        public IActionResult Add(Customers customer)
        {
            // TRÈS IMPORTANT : On ignore la validation des objets de navigation
            // Sinon ModelState.IsValid sera toujours faux
            ModelState.Remove("membershiptypes");
            ModelState.Remove("movies");
            ViewBag.Errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
           
            if (ModelState.IsValid)
            {
                _context.customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Si on arrive ici, c'est qu'il y a une erreur
            return View(customer);
        }

        public IActionResult Details(int id)
        {
            // On cherche le client avec son ID et on inclut son type d'abonnement
            var client = _context.customers
                .Include(c => c.membershiptypes)
                .FirstOrDefault(c => c.Id == id);

            if (client == null) return NotFound();

            return View(client);
        }
        
    }
}