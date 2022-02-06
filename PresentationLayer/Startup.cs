
using BLL.Interfaces;
using BLL.Services;
using DAL;
using LibraryAudit.PL.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PresentationLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContex>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers(optiotions => 
            {
                optiotions.Filters.Add(typeof(LastVisitTimeFilter));
            });
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IArchiveService, ArchiveService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IExport, CsvExport>();
            services.AddTransient<ExportService>();
            services.AddTransient<IExport, CsvExport>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<ISearchBookService, SearchBookService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseDefaultFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
