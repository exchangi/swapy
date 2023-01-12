using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using swapy.Models;
using Microsoft.AspNetCore.Http; //session4
using Microsoft.AspNetCore.Identity; //password hashing

namespace swapy.Controllers;

public class HomeController : Controller
{
    private MyContext _context; //additional
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        // _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("/login")]
      public IActionResult Login()
    {
        return View();
    }
    [HttpGet("/register")]
      public IActionResult Register()
    {
        return View();
    }
    
   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
