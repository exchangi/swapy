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

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    
    // public IActionResult Profile()
    // {
    //     if(HttpContext.Session.GetInt32("userId") == null) {
    //         return redirectToAction("Login");
    //     }
    //     return View();
    // }

    public IActionResult Privacy()
    {
        return View();
    }
    // -------profile-------------
      [HttpGet("/profile")]
    public IActionResult Profile()
    {
        return View();
    }
// --------about----------
    [HttpGet("/about")]
    public IActionResult About()
    {
        return View();
    }

    // ------------------USER CONTROLLER------------------

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
    // ----------------REGISTER-----------------
    //  [HttpPost("/user/create")]
    public IActionResult CreateUser (User NewUser)
    {
        if(ModelState.IsValid)
        {
            if(_context.Users.Any(u =>u.Email == NewUser.Email))
            {
                ModelState.AddModelError("Email" ,"Email Already exist !!");
            }
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
            _context.Add(NewUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId" ,NewUser.UserId);
            return RedirectToAction ("Index");
        } else {
            return View("Register");
        }
    }
    // -----------------LOGIN USER-------------------
       
    public IActionResult UserLogin (UserLogin UserSubmission)
    {
        if(ModelState.IsValid)
        {
            User? UserFromDB =_context.Users.FirstOrDefault(u => u.Email ==UserSubmission.LoginEmail);
            if(UserFromDB == null)
            {
                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                return View("Login");
            }
            var hasher = new PasswordHasher<UserLogin>();
            var result = hasher.VerifyHashedPassword(UserSubmission, UserFromDB.Password, UserSubmission.LoginPassword);
            if (result==0)
            {
                ModelState.AddModelError("LoginPassword", "Invalid Email/password");
                return View("Login");
            }
            HttpContext.Session.SetInt32("userId", UserFromDB.UserId);
            return RedirectToAction("Index");
        }
        return View("Login");
    }
// LogOut 
    [HttpGet("/logout")]
    public IActionResult Logout ()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

// --------------add product-----------
    [HttpGet("/add")]
    public IActionResult AddProduct()
    {
        ViewBag.ALLcategories = _context.Categories.ToList();
        return View();
    }
    public IActionResult AddNewProduct(Product newProduct)
    {
        if(ModelState.IsValid)
        {
            newProduct.UserId = (int)HttpContext.Session.GetInt32("userId");
            _context.Add(newProduct);
            _context.SaveChanges();
            ViewBag.AllProducts =_context.Products.ToList();
            return RedirectToAction("Profile");
        }
        ViewBag.AllProducts = _context.Products.ToList();
        return View("Index");
    }

   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
