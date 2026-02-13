using Microsoft.AspNetCore.Http;
using TP3.Models;
namespace TP3.ViewModels;

public class MoviesVM
{
    public Movies movie { get; set; }
    public IFormFile photo { get; set; }
}