using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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
            
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));


            services.AddControllers();
            services.AddMvcCore().AddApiExplorer();
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


            //User
            services.AddSingleton<IUserDAO, UserDAO>();
            services.AddSingleton<IUserService, UserService>();
            
            //Student
            services.AddSingleton<IStudentDAO, StudentDAO>();
            services.AddSingleton<IStudentService, StudentService>();

            //Company
            services.AddSingleton<ICompanyDAO, CompanyDAO>();
            services.AddSingleton<ICompanyService, CompanyService>();

            //Studiegebied
            services.AddSingleton<IStudiegebiedDAO, StudiegebiedDAO>();
            services.AddSingleton<IStudiegebiedService, StudiegebiedService>();

            //Opleiding
            services.AddSingleton<IOpleidingDAO, OpleidingDAO>();
            services.AddSingleton<IOpleidingService, OpleidingService>();

            //Afstudeerrichting
            services.AddSingleton<IAfstudeerrichtingDAO, AfstudeerrichtingDAO>();
            services.AddSingleton<IAfstudeerrichtingService, AfstudeerrichtingService>();

            //Keuze
            services.AddSingleton<IKeuzeDAO, KeuzeDAO>();
            services.AddSingleton<IKeuzeService, KeuzeService>();

            //Role
            services.AddSingleton<IRoleDAO, RoleDAO>();
            services.AddSingleton<IRoleService, RoleService>();

            //Offer
            services.AddSingleton<IOfferDAO, OfferDAO>();
            services.AddSingleton<IOfferService, OfferService>();

            //Favorite
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

            app.UseCors("MyPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
            //    RequestPath = new PathString("/Resources")
            //});

        }
    }
}
