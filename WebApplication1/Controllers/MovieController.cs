using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Controllers;


using WebApplication1.Models;
using WebApplication1.Models.ViewModel;

[Route("Movie")]
public class MovieController : Controller
{
    
        [HttpGet("Index/{id}")]
        public IActionResult Index()
        {
            var client = new Customer
            {
                id = 1,
                Name = "Dorra"
            };

            var films = new List<Movie>
            {
                new Movie { Id = 2, Name = "Movie 2" },
                new Movie { Id = 3, Name = "Movie 3" },
                new Movie { Id = 4, Name = "Movie 4" }
            };

            var viewModel = new CustomerMoviesVM
            {
                customer = client,
                movies = films,
            };

            return View(viewModel);
        }

        // GET: /Movie/Edit/2
        public IActionResult Edit(int id)
        {
            return Content("Test Id " + id);
        }

        // GET: /Movie/released/03/2020
        [HttpGet("released/{month}/{year}")]
        public IActionResult ByRelease(int year, int month)
        {
            return Content($"Test Release {month}/{year}");
        }
    }




    