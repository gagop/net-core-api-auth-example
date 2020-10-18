using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCoreApiExample.Infrastructure;
using NetCoreApiExample.Models;
using System;
using System.Text;

namespace NetCoreApiExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomDbContext>(options => options.UseInMemoryDatabase("ExampleDb"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidIssuer = "https://localhost:5001", //should come from configuration
                            ValidAudience = "https://localhost:5001", //should come from configuration
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                        };
                    });

            services.AddControllers();
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                CustomDbContext context = serviceScope.ServiceProvider.GetService<CustomDbContext>();
                AddTestData(context);
            }
        }

        private static void AddTestData(CustomDbContext context)
        {
            AppUser appUser = new AppUser
            {
                Email = "kowalski@wp.pl",
                EmailConfirmed = true,
                UserName = "kowal"
            };

            PasswordHasher<AppUser> pass = new PasswordHasher<AppUser>();
            appUser.PasswordHash = pass.HashPassword(appUser, "asd123");

            context.Users.Add(appUser);
            context.SaveChanges();
        }
    }
}