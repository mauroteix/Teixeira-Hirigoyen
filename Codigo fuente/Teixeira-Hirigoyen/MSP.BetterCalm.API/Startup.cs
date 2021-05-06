using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MSP.BetterCalm.API.Filters;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccess;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MSP.BetterCalm.API
{
    [ExcludeFromCodeCoverage]
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BetterCalm", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddDbContext<BetterCalmContext>();
            services.AddScoped<IData<Category>, CategoryRepository>();
            services.AddScoped<ICategoryLogic, CategoryLogic>();
            services.AddScoped<IData<Playlist>, PlaylistRepository>();
            services.AddScoped<IPlaylistLogic, PlaylistLogic>();
            services.AddScoped<PlaylistCategory>();
            services.AddScoped<IData<User>, UserRepository>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IData<MedicalCondition>, MedicalConditionRepository>();
            services.AddScoped<IMedicalConditionLogic, MedicalConditionLogic>();
            services.AddScoped<IData<Track>, TrackRepository>();
            services.AddScoped<ITrackLogic, TrackLogic>();
            services.AddScoped<IData<Psychologist>, PsychologistRepository>();
            services.AddScoped<IPsychologistLogic, PsychologistLogic>();
            services.AddScoped<IData<Administrator>, AdministratorRepository>();
            services.AddScoped<IAdministratorLogic, AdministratorLogic>();
            services.AddScoped<ISessionLogic, SessionLogic>();
            services.AddScoped<AuthorizationFilter>();

            services.AddControllers().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BetterCalm API V1");
                //c.RoutePrefix = string.Empty;
            });
        }
    }
}
