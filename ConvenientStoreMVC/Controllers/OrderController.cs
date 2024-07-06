using ConvenientStoreAPI.Models;
using ConvenientStoreMVC.CallService;
using ConvenientStoreMVC.ChartImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Web.UI.DataVisualization.Charting;

namespace ConvenientStoreMVC.Controllers
{
    public class OrderController : Controller
    {
        private ConvenientStoreContext context;
        private OrderService orderService;
        public OrderController(ConvenientStoreContext context, OrderService orderService)
        {
            this.context = context;
            this.orderService = orderService;
        }
        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            ViewData["list"] = await orderService.getAll();
            return View();
        }

        public async Task<IActionResult> Process(int id)
        {
            return RedirectToAction("Index");
        }
        

        // GET: OrderController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await orderService.getById(id));
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
