using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using swapy.Models;
using Microsoft.AspNetCore.Http; //session4
using Microsoft.AspNetCore.Identity; //password hashing
using Microsoft.EntityFrameworkCore;

namespace swapy.Controllers;

public class HomeController : Controller
{
    private MyContext _context; //additional
    private readonly ILogger<HomeController> _logger;
    public List<Product> MyCard = new List<Product>();
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    // ***SHOW product **

       public IActionResult Index()
    {
        ViewBag.ALLcategories= _context.Categories.ToList();
        ViewBag.AllProducts = _context.Products.ToList();
        return View();
    }
// ----------------edit profile----------

        [HttpGet("/edit/{UserId}")]
    public IActionResult EditUser(int UserId)
    {
        User model = _context.Users.FirstOrDefault(d => d.UserId == UserId);
        if (model == null)
            return RedirectToAction("Index");
        return View(model);
    }
    [HttpPost("{userId}/update")]
    public IActionResult UpdateUser(User user, int UserId)
    {
        User toUpdate = _context.Users.FirstOrDefault(d => d.UserId == UserId);
        if (ModelState.IsValid)
        {
            toUpdate.FirstName = user.FirstName;
            toUpdate.LastName = user.LastName;
            toUpdate.Email = user.Email;
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            toUpdate.Password = Hasher.HashPassword(user, user.Password);
            toUpdate.ConfirmPassword = Hasher.HashPassword(user, user.ConfirmPassword);
            toUpdate.Image = user.Image;
            toUpdate.Phone = user.Phone;
            toUpdate.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("EditUser", user);
    }

    // -------show category-----
    [HttpGet("/ShowCat/{CategoryId}")]
    public IActionResult ShowCat(int CategoryId)
    {
        // List<Product> NotMyProduct = _context.Products.Where(c=> !c.ProductCategories.Any(p=>p.CategoryId==CategoryId)).ToList();
        // List<Product> CategoryProducts = _context.Products.Include(p => p.Category).ToList();
        List<Product> CategoryProducts = _context.Products.Include(p => p.Category).Where(c=>c.CategoryId == CategoryId ).ToList();
        ViewBag.CategoryProducts = CategoryProducts;
        return View(ShowCat);
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
      [HttpGet("/profile/{UserId}")]
    public IActionResult Profile(int UserId)
    {
        User? loggedUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.AllProducts = _context.Products.Where(p => p.UserId == HttpContext.Session.GetInt32("userId")).ToList();
        return View(loggedUser);
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
// -------------LogOut -------------
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
            var userId = (int)HttpContext.Session.GetInt32("userId");
            return RedirectToAction("Profile", userId);
        }    
        ViewBag.AllProducts = _context.Products.ToList();
        return View("Index");
    }
    // Add Product To Card 
    // [HttpPost("/addtocard/{productId}")]
    // public IActionResult AddProductToCard(int productId)
    // {
    //     Product? product = _context.Products.FirstOrDefault(p=> p.ProductId == productId);
    //     if(product != null)
    //     {
    //         MyCard.Add(product);
    //     }
    //     return 
    // }

   // SWAP
//    [HttpPost("/swap/{productId}")]
//     public IActionResult Swap(int productId) {
//         Product SwappedProduct = _context.Products.FirstOrDefault(p => p.ProductId == productId);

//     }
// ---------Swap Form--------
[HttpGet("/swap")]
 public IActionResult SwapForm()
    {
        ViewBag.ALLcategories = _context.Categories.ToList();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
