
using Autofac.Extensions.DependencyInjection;
using Autofac;
using ProductApp.Repo.Abstraction;
using ProductApp.Mapping;
using ProductApp.Repo;
using ProductApp.DB;

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
            builder.Services.AddMemoryCache(x => x.TrackStatistics = true);
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
