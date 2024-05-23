
using Autofac.Extensions.DependencyInjection;
using Autofac;
using ProductApp.Repo.Abstraction;
using ProductApp.Mapping;
using ProductApp.Repo;
using ProductApp.DB;
using Microsoft.Extensions.FileProviders;

namespace ProductApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddMemoryCache(options => options.TrackStatistics = true);
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new ProductContext(cfg.GetConnectionString("ProductDB"))).InstancePerDependency();
                cb.RegisterType<ProductRepo>().As<IProductRepo>();
                cb.RegisterType<ProductGroupRepo>().As<IProductGroupRepo>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var staticFilePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
            Directory.CreateDirectory(staticFilePath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilePath),
                RequestPath = "/static"
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
