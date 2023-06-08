using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;

  public HomeController(ILogger<HomeController> logger)
  {
    _logger = logger;
  }

  [HttpGet("")]
  public IActionResult Index()
  {
    return View();
  }

  [HttpPost("login")]
  public IActionResult Login(User user)
  {
    if (ModelState.IsValid)
    {
      HttpContext.Session.SetString("UserName", user.Name);
      HttpContext.Session.SetInt32("Number", 0);
      return RedirectToAction("Dashboard");
    }
    else
    {
      return View("Index");
    }
  }

  [HttpGet("dashboard")]
  public IActionResult Dashboard()
  {
    string? userName = HttpContext.Session.GetString("UserName");
    if (string.IsNullOrEmpty(userName))
    {
      return RedirectToAction("Index");
    }
    ViewBag.UserName = userName;
    ViewBag.Number = HttpContext.Session.GetInt32("Number") ?? 0;
    return View();
  }

  [HttpPost("dashboard/update")]
  public IActionResult Update(string operation)
  {
    int number = HttpContext.Session.GetInt32("Number") ?? 0;
    if (operation == "increment")
    {
      number += 1;
    }
    else if (operation == "decrement")
    {
      number -= 1;
    }
    else if (operation == "multiply")
    {
      number *= 2;
    }
    else if (operation == "random")
    {
      Random random = new Random();
      int randomValue = random.Next(1, 11);
      number += randomValue;
    }
    HttpContext.Session.SetInt32("Number", number);
    return RedirectToAction("Dashboard");
  }

  [HttpGet("logout")]
  public IActionResult Logout()
  {
    HttpContext.Session.Clear();
    return View("Index");
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
