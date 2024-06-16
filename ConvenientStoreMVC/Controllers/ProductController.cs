using ConvenientStoreMVC.Common;
using ConvenientStoreAPI.Common;
using ConvenientStoreAPI.Models;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.OpenApi.Extensions;
using System.Net;

namespace ConvenientStoreMVC.Controllers
{
    public class ProductController : Controller
    {
        private PhotoManager manager;

        public ProductController(PhotoManager manager)
        {
            this.manager = manager;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            string a = APIEnum.BASE_URL.GetDescription();
            List<Product> p = APIEnum.BASE_URL.GetDescription().AppendPathSegment(APIEnum.PRODUCT.GetDescription())
               .WithSettings(s => s.JsonSerializer = Serializer.newtonsoft)
               .GetJsonAsync<List<Product>>().Result;
            ConvenientStoreContext context = new ConvenientStoreContext();
            Image img = context.Images.Find(1);
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].ImageId == null) p[i].Image = img;
            }
            ViewData["list"] = p;
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.sup = APIEnum.BASE_URL.GetDescription().AppendPathSegment(APIEnum.SUPPLIER.GetDescription())
                .WithSettings(s => s.JsonSerializer = Serializer.newtonsoft)
               .GetJsonAsync<List<Supplier>>().Result.Select(c => new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Name,
               });
            ViewBag.cat = APIEnum.BASE_URL.GetDescription().AppendPathSegment(APIEnum.CATEGORY.GetDescription())
                .WithSettings(s => s.JsonSerializer = Serializer.newtonsoft)
               .GetJsonAsync<List<Category>>().Result.Select(c => new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Name,
               });
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        public ActionResult Create(Product product, IFormFile image)
        {
            try
            {
                //IFormFile img = f["image"];
                Image result = manager.UploadImage(image);
                product.ImageId = result.Id;

                IFlurlResponse resp =  APIEnum.BASE_URL.GetDescription().AppendPathSegment(APIEnum.PRODUCT.GetDescription())
                .WithSettings(s => s.JsonSerializer = Serializer.newtonsoft).PostJsonAsync(product).Result;

                if(resp.StatusCode == ((int)HttpStatusCode.Created))
                {
                    TempData["mess"] = "Created Product Successfully";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
