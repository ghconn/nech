using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Chloe;
using common.mdl;
using Microsoft.AspNetCore.Mvc;
using mvcapp.Models;
using Microsoft.Extensions.Logging;

namespace mvcapp.Controllers
{
    public class HomeController : Controller
    {
        IDbContext _sqlcontext;
        ILogger<HomeController> _logger;
        
        public HomeController(IDbContext sqlcontext, ILogger<HomeController> logger)
        {
            _sqlcontext = sqlcontext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var city = _sqlcontext.Query<tRuleDefine>().Select(r => r.rCity).First();
            ViewBag.city = city;
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
