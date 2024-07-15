using ConvenientStoreAPI.Common;
using ConvenientStoreAPI.Mapper;
using ConvenientStoreAPI.Models;
using ConvenientStoreAPI.Serializer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.ModelBuilder;
using System.Text.Json.Serialization;

namespace ConvenientStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var odataBuilder = new ODataConventionModelBuilder();
            builder.Services.AddControllers().AddOData(opt => opt

            .Select()
            .Expand()
            .Filter()
            .OrderBy()
            .Count()
            .SetMaxTop(100)
            .AddRouteComponents("odata", odataBuilder.GetEdmModel())
            ).AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            //.AddNewtonsoftJson(options =>
            //{
            //    //options.SerializerSettings.ContractResolver = new IgnoreVirtualContractResolver();
            //    options.SerializerSettings.Converters.Add(new IgnoreVirtualContractResolver());
            //})

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IConfiguration>(configuration);
            builder.Services.AddDbContext<ConvenientStoreContext>();
            builder.Services.AddAutoMapper(typeof(MyMapper).Assembly);
            builder.Services.AddScoped<PhotoManager>();

            var app = builder.Build();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

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
