using System.IO;
using AutoMapper;
using CareebizExam.Helpers;
using CareebizExam.Infrastructure;
using CareebizExam.Logic;
using CareebizExam.Model;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareebizExam.Extensions
{
    public static class ServiceExtensions
    {
       
            public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
            {
                var connectionString = config.GetConnectionString("CareebizExamEntities");
                services.AddDbContext<CareebizExamDbContext>(o => o.UseSqlServer(connectionString));
            }



        public static void ConfigureMapping(this IServiceCollection services)

        {
            //Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        public static void ConfigureUnitOfWork(this IServiceCollection services)
            {
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<IShapesLogic, ShapesLogic>();
                
            }
        public static void ConfigurePDF(this IServiceCollection services)
            {
                services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            var context = new CustomAssemblyLoadContext();
                context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

        }

      

    }
}
