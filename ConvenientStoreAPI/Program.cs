using ConvenientStoreAPI.Common;
using ConvenientStoreAPI.Models;
using ConvenientStoreAPI.Serializer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;

namespace ConvenientStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddOData(opt => opt
            .Select()
            .Expand()
            .Filter()
            .OrderBy()
            .SetMaxTop(100)
            ).AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            //.AddNewtonsoftJson(options =>
            //{
            //    //options.SerializerSettings.ContractResolver = new IgnoreVirtualContractResolver();
            //    options.SerializerSettings.Converters.Add(new IgnoreVirtualContractResolver());
            //})
            ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IConfiguration>(configuration);
            builder.Services.AddDbContext<ConvenientStoreContext>();
            builder.Services.AddScoped<PhotoManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
