using ConvenientStoreAPI.Models;
using ConvenientStoreMVC.CallService;
using ConvenientStoreMVC.ChartImpl;
using ConvenientStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ConvenientStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private OrderService orderService;
        private SupplierService supplierService;
        private UserService userService;
        public HomeController(SupplierService service, OrderService orderService, UserService userService)
        {
            this.orderService = orderService;
            this.supplierService = service;
            this.userService = userService;
        }
        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            List<Order> orders = await orderService.getAll();
            ViewData["orders"] = orders;
            ViewData["chart"] = await DisplayChart();
            ViewData["revenue"] = orders.SelectMany(x => x.Orderdetails).Sum(o => (o.Product.Price * o.Quantity));
            ViewData["sales"] = orders.SelectMany(x => x.Orderdetails).Sum(o => o.Quantity);
            ViewData["client"] = userService.getAllInSale().Result.Count;
            ViewData["pending"] = orders.Where(x => !x.IsProcess).Count();

            return View();
        }
        private string[] buildColor(int num)
        {
            Random rand = new Random();
            List<string> colors = new List<string>();
            while (num > 0)
            {
                num--;
                colors.Add($"rgba({rand.Next(0, 255)}, {rand.Next(0, 255)}, {rand.Next(0, 255)}, {rand.NextDouble()})");
            }
            return colors.ToArray();
        }
        public async Task<ChartJsViewModel> DisplayChart()
        {
            Random rand = new Random();
            // Ref: https://www.chartjs.org/docs/latest/
            List<Supplier> suppliersInSale = await supplierService.getSuppliersInSale();
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
                        backgroundColor = buildColor(suppliersInSale.Count),
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
            ChartJs chart = chartData;

            ChartJsViewModel chartModel = new ChartJsViewModel
            {
                Chart = chart,
                ChartJson = JsonConvert.SerializeObject(chart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                })
            };

            return chartModel;
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
