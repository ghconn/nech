using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Chloe;
using common.mdl;
using Microsoft.AspNetCore.Mvc;
using mvcapp.Models;

namespace mvcapp.Controllers
{
    public class HomeController : Controller
    {
        IDbContext _sqlcontext;
        public HomeController(IDbContext sqlcontext)
        {
            _sqlcontext = sqlcontext;
        }

        public IActionResult Index()
        {
            var rule = _sqlcontext.Query<tRuleDefine>().First();
            ViewBag.city = rule?.rCity;
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
