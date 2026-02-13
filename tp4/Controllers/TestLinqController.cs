using Microsoft.AspNetCore.Mvc;
using tp4.Services.ServiceContracts;

namespace tp4.Controllers;

public class TestLinqController : Controller
{
    private readonly IMovieService _movieService;
    private readonly ICustomerService _customerService;
    private readonly IGenreService _genreService;

    public TestLinqController(IMovieService movieService, ICustomerService customerService, IGenreService genreService)
    {
        _movieService = movieService;
        _customerService = customerService;
        _genreService = genreService;
    }
    // GET: /TestLinq/Question1
    public IActionResult Question1()
    {
        var actionMovies = _movieService.GetActionMoviesInStock();
        return View(actionMovies);
    }
    public IActionResult Question2()
    {
        var moviesOrdered = _movieService.GetMoviesOrderedByDateAndTitle();
        return View(moviesOrdered);
    }

    public IActionResult Question3()
    {
        var totalMovies = _movieService.GetTotalMoviesCount();
        ViewBag.TotalMovies = totalMovies;
        return View();
    }
    public IActionResult Question4()
    {
        var subscribedCustomers = _customerService.GetSubscribedCustomersWithHighDiscount();
        return View(subscribedCustomers);
    }
    // GET: /TestLinq/Question5
    public IActionResult Question5()
    {
        var moviesWithGenre = _movieService.GetMoviesWithGenre();
        return View(moviesWithGenre);
    }
    public IActionResult Question6()
    {
        var topGenres = _genreService.GetTopThreePopularGenres();
        return View(topGenres);
    }
}


