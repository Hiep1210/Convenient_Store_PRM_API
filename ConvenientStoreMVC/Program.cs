using ConvenientStoreAPI.Common;
using ConvenientStoreAPI.Mapper;
using ConvenientStoreAPI.Models;
using ConvenientStoreMVC.CallService;

namespace ConvenientStoreMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var apiProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ConvenientStoreAPI");

            builder.Configuration
                .SetBasePath(apiProjectPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddDbContext<ConvenientStoreContext>();
            builder.Services.AddAutoMapper(typeof(MyMapper).Assembly);
            builder.Services.AddScoped<PhotoManager>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<SupplierService>();
            builder.Services.AddScoped<UserService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}