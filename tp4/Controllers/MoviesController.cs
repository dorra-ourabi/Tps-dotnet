using Microsoft.AspNetCore.Mvc;
using tp4.Services.ServiceContracts;

namespace tp4.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    // GET: Movies
    public IActionResult Index()
    {
        var movies = _movieService.GetAllMovies();
        return View(movies);
    }

    // GET: Movies/Details/5
    public IActionResult Details(int id)
    {
        var movie = _movieService.GetMovieById(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }
}