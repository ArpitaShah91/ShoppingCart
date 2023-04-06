using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Models;
using System;
using System.Diagnostics;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        public Checkout checkout = new Checkout();

        public IActionResult Index()
        {

            //CASE 1 :
            checkout.Scan("atv");
            checkout.Scan("atv");
            checkout.Scan("atv");
            checkout.Scan("vga");

            ViewBag.Scenario1 = checkout.TotalPrice();

            checkout.Delete();

            //CASE 2:
            checkout.Scan("atv");
            checkout.Scan("ipd");
            checkout.Scan("ipd");
            checkout.Scan("atv");
            checkout.Scan("ipd");
            checkout.Scan("ipd");
            checkout.Scan("ipd");

            ViewBag.Scenario2 = checkout.TotalPrice();

            checkout.Delete();

            //CASE 3:
            checkout.Scan("mbp");
            checkout.Scan("vga");
            checkout.Scan("ipd");

            ViewBag.Scenario3 = checkout.TotalPrice();
            checkout.Delete();

            return View();
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
}
