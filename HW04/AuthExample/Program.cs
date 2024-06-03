
using AuthExample.Repository;
using AuthExample.Security;
using AuthExample.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Autofac;
using AuthExample.Context;
using Autofac.Extensions.DependencyInjection;

namespace AuthExample
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
            builder.Services.AddSwaggerGen(
               options =>
               {
                   options.AddSecurityDefinition(
                       JwtBearerDefaults.AuthenticationScheme,
                       new()
                       {
                           In = ParameterLocation.Header,
                           Description = "Please insert jwt with Bearer into filed",
                           Name = "Authorization",
                           Type = SecuritySchemeType.Http,
                           BearerFormat = "Jwt Token",
                           Scheme = JwtBearerDefaults.AuthenticationScheme
                       });
                   options.AddSecurityRequirement(
                       new()
                       {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = JwtBearerDefaults.AuthenticationScheme
                                    }
                                },
                                new List<string>()
                            }

                       });
               });
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new UserContext(cfg.GetConnectionString("UserDB"))).InstancePerDependency();
                cb.RegisterType<TokenService>().As<ITokenService>();
                cb.RegisterType<UserRepository>().As<IUserRepository>();
            });
            
            var jwt = builder.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>()
                      ?? throw new Exception("JwtConfiguration not found");
            builder.Services.AddSingleton(provider => jwt);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    //IssuerSigningKey = jwt.GetSigningKey()

                    IssuerSigningKey = new RsaSecurityKey(RSATools.GetPublicKey())
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
