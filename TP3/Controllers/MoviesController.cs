using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using TP3.Models;
using TP3.ViewModels; // Pour trouver MoviesVM
using System.IO;
using Microsoft.AspNetCore.Hosting; // Parfois nécessaire selon la version
// OU 
using Microsoft.Extensions.Hosting;
namespace TP3.Controllers;

public class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;
    // ÉTAPE 1 : Déclaration
    private readonly IWebHostEnvironment _webHostEnvironment;
    public MoviesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {
        
        
        // On récupère les films EN INCLUANT les données du Genre lié
            var moviesWithGenres = _context.movies
                .Include(m => m.Genre) 
                .ToList();

            // On transforme en liste de ViewModels
            

            return View(moviesWithGenres);
        
    }
   // Cette méthode permet d'afficher le formulaire quand on tape l'URL
    [HttpGet]
    public IActionResult Create()
    {
        // On peut passer un modèle vide pour éviter les erreurs dans la vue
        var model = new MoviesVM(); 
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MoviesVM model, IFormFile photo)
    {
        if (photo == null)
            return Content("File not uploaded");
        try
        {
//Combine trois chaînes dans un seul path
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images",
                photo.FileName);
//fournit un stream pour la lecture et ecriture dans un fichier
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                photo.CopyTo(stream);
                stream.Close();
            }
            model.movie.ImageFile = photo.FileName;
//Mapping entre Model et ViewModel
            var movie = new Movies
            {
                Id = new int(),
                Name = model.movie.Name,
                DateAjoutMovie = model.movie.DateAjoutMovie,
                ImageFile = photo.FileName,
            };
            _context.Add(movie);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            throw;
        }
    }
}