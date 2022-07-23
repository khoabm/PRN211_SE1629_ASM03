using BusinessObject.DataAccess;
using DataAccess.Repository;
using eStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eStore.Controllers
{
    public class HomeController : Controller
    {
        private IMemberRepository memberRepository = new MemberRepository();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int? memberId = HttpContext.Session.GetInt32("user");
            if (memberId == null)
                return RedirectToAction(nameof(Login));
            if (memberId == 0)
                ViewBag.Member = "Admin";
            else
            {
                ViewBag.Member = "Member";
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            try
            {
                Member member = memberRepository.checkLogin(email, password);
                HttpContext.Session.SetInt32("user", member.MemberId);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ViewBag.Message = "Incorrect Email or Password";
                return View();
            }
        }

        public ActionResult Logout()
        {
            var session = HttpContext.Session;
            if (session.GetInt32("user") != null)
            {
                session.Clear();
            }
            return RedirectToAction(nameof(Login));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}