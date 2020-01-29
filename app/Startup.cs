using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using src.SampleData.FromDataBase;
using src.SampleData.Common;
using src.SampleData.FromFile;
using Microsoft.EntityFrameworkCore;

namespace app
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            var repositoryOptions = new RepositoryOptions();
            Configuration.GetSection("RepositoryOptions").Bind(repositoryOptions);
            services.AddSingleton<RepositoryOptions>(repositoryOptions);

            if (repositoryOptions.RepositoryType == RepositoryType.InSqlServer)
            {
                services.AddDbContext<DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                services.AddScoped<IPersonsRepository, SqlServerAccessRepository>();
                services.AddScoped<ISampleDataRetrieval, SampleDataRetrieval>();
                services.AddScoped<ISampleDataManager, SampleDataManager>();
            }
            else if (repositoryOptions.RepositoryType == RepositoryType.InMemory)
            {
                services.Configure<FileOptions>(Configuration.GetSection("FileOptions"));
                services.AddSingleton<ISampleDataRetrieval, SampleDataRetrieval>();
                services.AddSingleton<IPersonsRepository, MemoryPersonsRepository>();
                services.AddSingleton<ISampleDataManager, SampleDataManager>();
            }
            else
            {
                throw new System.Exception("Configuration was not read correctly");
            }
            
            services.AddControllers();
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
        }
    }
}
