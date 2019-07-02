using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Api.Entities;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CityInfo.Api
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()))
                    ;
            //.AddJsonOptions(o => {
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver
            //           as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }
            //});

            string connectionString = Startup.Configuration["connectionStrings:cityInfoConnectionString"];



            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));
#if DEBUG
            services.AddTransient<IMailService,LocalMailService>();
#else
            services.AddTransient<IMailService,CloudMailService>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CityInfoContext cityInfoContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            cityInfoContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<City, Model.CityWithoutPointOfInterestDto>();
                cfg.CreateMap<City, Model.CityDto>();
                cfg.CreateMap<PointOfInterests, Model.PointOfInterestsDto>();
                cfg.CreateMap<Model.PointOfInterestsForCreationDto, PointOfInterests>();
                cfg.CreateMap<Model.PointOfInterestsForUpdateDto, PointOfInterests>();
            });

            app.UseMvc();


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hesssllo World!");
            });
        }
    }
}
