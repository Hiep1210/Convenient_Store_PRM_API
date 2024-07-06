using ConvenientStoreAPI.Common;
using ConvenientStoreAPI.Models;
using ConvenientStoreMVC.Common;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConvenientStoreMVC.Controllers
{
    public class UserController : Controller
    {
        IConfiguration configuration;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            User result = getUser(user);
            if(result != null)
            {
                string username = configuration.GetSection("Admin").GetSection("User").Value;
                string pass = configuration.GetSection("Admin").GetSection("Pass").Value;
                if(!(username.Equals(result.Name) && pass.Equals(result.Password)))
                {
                    TempData["mess"] = "Failed to login";
                    return RedirectToAction("LogIn");
                }
            }
            TempData["mess"] = $"Welcome Back {result.Name} !";
            return RedirectToAction("Index", "Home");
        }

        private User getUser(User user)
        {
            List<User> result = APIEnum.BASE_URL.GetDescription().AppendPathSegment(APIEnum.USER.GetDescription()).AppendQueryParam($"$filter=name eq '{user.Name}' and password eq '{user.Password}'")
               .WithSettings(s => s.JsonSerializer = Serializer.newtonsoft)
               .GetJsonAsync<List<User>>().Result;
            if(result == null || result.Count <= 0)
            {
                return new User();
            }
            return result.First();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
