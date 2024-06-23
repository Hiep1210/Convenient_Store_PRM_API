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
using Newtonsoft.Json;
using ConvenientStoreAPI.Mapper.DTO;
using AutoMapper;

namespace ConvenientStoreMVC.Controllers
{
    public class ProductController : Controller
    {
        private PhotoManager manager;
        private readonly IMapper mapper;

        public ProductController(PhotoManager manager, IMapper mapper)
        {
            this.manager = manager;
            this.mapper = mapper;
        }
        // GET: ProductController
        public ActionResult Index(int pages = 1)
        {
            string a = APIEnum.BASE_URL.GetDescription();
            List<Product> p = APIEnum.BASE_URL.GetDescription().AppendPathSegment(APIEnum.PRODUCT.GetDescription())
               .WithSettings(s => s.JsonSerializer = Serializer.newtonsoft)
               .GetJsonAsync<List<Product>>().Result;
            ViewData["pages"] = Convert.ToInt32(Math.Ceiling((double)p.Count / (int)SizeEnum.PAGE_SIZE));
            ConvenientStoreContext context = new ConvenientStoreContext();
            Image img = context.Images.Find(1);
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].ImageId == null) p[i].Image = img;
            }
            ViewData["list"] = p.Skip((pages - 1) * (int)SizeEnum.PAGE_SIZE).Take((int)SizeEnum.PAGE_SIZE).ToList();
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
        public ActionResult Create(ProductRequest product, IFormFile image)
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
            Product p = getProduct(id);
            ViewData["img"] = p.Image;
            ProductRequest request = mapper.Map<ProductRequest>(p);
            return View(request);
        }

        private Product getProduct(int id)
        {
            Product p = APIEnum.BASE_URL.GetDescription().AppendPathSegment(APIEnum.PRODUCT.GetDescription()).AppendPathSegment(id)
               .WithSettings(s => s.JsonSerializer = Serializer.newtonsoft)
               .GetJsonAsync<Product>().Result;
            return p;
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductRequest request, IFormFile image)
        {
            try
            {
                Image result = manager.UploadImage(image);
                request.ImageId = result.Id;

                IFlurlResponse resp = APIEnum.BASE_URL.GetDescription().AppendPathSegment(APIEnum.PRODUCT.GetDescription()).AppendPathSegment(request.Id)
                .WithSettings(s => s.JsonSerializer = Serializer.newtonsoft).PutJsonAsync(request).Result;

                if (resp.StatusCode == ((int)HttpStatusCode.NoContent))
                {
                    TempData["mess"] = "Update Product Successfully";
                }
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
            Product p = getProduct(id);
            return View(p);
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
