using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Youngpotentials.DAO;
using Youngpotentials.Service;
using YoungpotentialsAPI.Helpers;

namespace YoungpotentialsAPI
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
            
            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        //var userId = context.Principal.Identity.Name;
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserDAO, UserDAO>();
            services.AddSingleton<IStudentDAO, StudentDAO>();
            services.AddSingleton<ICompanyDAO, CompanyDAO>();
            services.AddSingleton<IStudentService, StudentService>();
            services.AddSingleton<ICompanyService, CompanyService>();
            services.AddSingleton<IStudiegebiedService, StudiegebiedService>();
            services.AddSingleton<IStudiegebiedDAO, StudiegebiedDAO>();
            services.AddSingleton<IOpleidingService, OpleidingService>();
            services.AddSingleton<IOpleidingDAO, OpleidingDAO>();
            services.AddSingleton<IAfstudeerrichtingService, AfstudeerrichtingService>();
            services.AddSingleton<IAfstudeerrichtingDAO, AfstudeerrichtingDAO>();
            services.AddSingleton<IKeuzeService, KeuzeService>();
            services.AddSingleton<IKeuzeDAO, KeuzeDAO>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IRoleDAO, RoleDAO>();
            services.AddSingleton<IOfferDAO, OfferDAO>();
            services.AddSingleton<IOfferService, OfferService>();
            services.AddSingleton<IFavoritesDAO, FavoritesDAO>();
            services.AddSingleton<IFavoritesService, FavoritesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
