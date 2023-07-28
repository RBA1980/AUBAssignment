using AUBAssignmentBusiness;
using AUBAssignmentBusiness.DataAccess;
using AUBAssignmentBusiness.Engines;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;

namespace AUBAssignmentServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter()));

            services.AddDbContext<AUBAssignmentDBContext>(
            options => SqlServerDbContextOptionsExtensions.UseSqlServer(options,
            Configuration.GetConnectionString("AUBAssignmentDB")));

            services.AddScoped<AUBAssignmentEngine>();

            services.AddHttpClient<RestClient>(c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("RestUrl"));
                
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Configuration.GetValue<string>("Bearer"));
                c.Timeout = TimeSpan.FromSeconds(300);
            });

            //services.AddMemoryCache();
            services.AddDistributedMemoryCache(); 
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AUBAssignmentServiceApp", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AUBAssignmentServiceApp v1"));

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
