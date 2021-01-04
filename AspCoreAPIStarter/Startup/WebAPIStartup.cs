using System;
using System.Text.Json.Serialization;
using AspBusiness.Providers;
using AspCoreAPIStarter.Handlers;
using AspDataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace AspCoreAPIStarter.Startup
{
    public class WebAPIStartup
    {
        public WebAPIStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(SwaggerConfig.ConfigSwagger);
            services.ConfigSecurity(Config<SecuritySettings>(services));
            services.AddDbContext<DataContext>(ConfigDb, ServiceLifetime.Transient, ServiceLifetime.Transient);
            services.RegisterDI();
            services.AddMvc(FilterHelper.Register)
                .AddJsonOptions(ConfigJson);
        }

        private static void ConfigJson(JsonOptions options)
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeJsonNamingPolicy();
        }

        private T Config<T>(IServiceCollection services) where T: class
        {
            var config = Activator.CreateInstance<T>();
            Configuration.Bind(typeof(T).Name, config);
            services.AddSingleton(config);
            return config;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<TokenProviderMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            //app.UseHttpsRedirection();
            app.ConfigSwagger();

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);

            app.UseStaticFiles();


        }

        private void ConfigDb(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}