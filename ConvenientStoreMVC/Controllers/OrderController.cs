using ConvenientStoreAPI.Models;
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
        public OrderController(ConvenientStoreContext context)
        {
            this.context = context;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            ViewData["list"] = context.Orders.Include(x => x.Orderdetails).ThenInclude(detail => detail.Product).ThenInclude(p => p.Supplier).ToList();
            return View();
        }
        public ActionResult DisplayChart()
        {
            // Ref: https://www.chartjs.org/docs/latest/
            List<Supplier> suppliersInSale = context.Suppliers.Include(x => x.Products).ThenInclude(x => x.Orderdetails)
                            .Where(supplier => supplier.Products
                                .Any(product => product.Orderdetails.Any())).ToList();
            ChartJs chartData = new ChartJs()
            {
                type = "bar",
                responsive = true,
                data = new Data()
                {
                    labels = suppliersInSale.Select(x => x.Name).ToArray(),
                    datasets = new Dataset[]
                 {
                    new Dataset()
                    {
                        label = "Suppliers Whom Make A Sale",
                        data = suppliersInSale.SelectMany(x => x.Products.Select(p => p.Orderdetails.Sum(or => Convert.ToInt32(or.Quantity)))).ToArray(),
                        backgroundColor = new [] {
                            "rgba(255, 99, 132, 0.2)",
                            "rgba(54, 162, 235, 0.2)",
                            "rgba(255, 206, 86, 0.2)",
                            "rgba(75, 192, 192, 0.2)",
                            "rgba(153, 102, 255, 0.2)",
                            "rgba(255, 159, 64, 0.2)"
                        },
                        borderColor = new [] {
                            "rgba(255, 99, 132, 1)",
                            "rgba(54, 162, 235, 1)",
                            "rgba(255, 206, 86, 1)",
                            "rgba(75, 192, 192, 1)",
                            "rgba(153, 102, 255, 1)",
                            "rgba(255, 159, 64, 1)"
                        },
                        borderWidth = 1
                    }
            }
                },
                options = new Options()
                {
                    scales = new Scales()
                    {
                        yAxes = new Yax[]
                        {
                            new Yax()
                            {
                                ticks = new Ticks { beginAtZero = true },
                            }
                        }
                    }
                }
            };

            //var chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
            var a = context.Suppliers.Include(x => x.Products).ThenInclude(x => x.Orderdetails)
                            .Where(supplier => supplier.Products
                                .Any(product => product.Orderdetails.Any())).ToArray();
            ChartJs chart = chartData;

            var chartModel = new ChartJsViewModel
            {
                Chart = chart,
                ChartJson = JsonConvert.SerializeObject(chart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                })
            };

            return View(chartModel);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
