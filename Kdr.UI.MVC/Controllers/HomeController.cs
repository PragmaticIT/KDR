using Kdr.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kdr.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthService _authService;

        public HomeController(IAuthService authService) {
            _authService = authService;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}