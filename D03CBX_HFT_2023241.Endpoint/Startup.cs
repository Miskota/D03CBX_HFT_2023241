using D03CBX_HFT_2023241.Logic;
using D03CBX_HFT_2023241.Models;
using D03CBX_HFT_2023241.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D03CBX_HFT_2023241.Endpoint {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {

            services.AddTransient<MusicDBContext>();

            services.AddTransient<IRepository<Record>, RecordRepository>();
            services.AddTransient<IRepository<Album>, AlbumRepository>();
            services.AddTransient<IRepository<Writer>, WriterRepository>();

            services.AddTransient<IRecordLogic, RecordLogic>();
            services.AddTransient<IAlbumLogic, AlbumLogic>();
            services.AddTransient<IWriterLogic, WriterLogic>();
            //services.AddSingleton if AddTransient doesn't work

            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "D03CBX_HFT_2023241.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "D03CBX_HFT_2023241.Endpoint v1"));
            }

            app.UseExceptionHandler(c => c.Run(async context => {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
