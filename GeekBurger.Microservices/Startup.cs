using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GeekBurger.Products
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services
                .AddMvc(options =>
                { options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter()); })
                .AddJsonOptions(options => options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_0);

            services.AddCors();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekBurger API V1");
                c.RoutePrefix = "";
            });

            app.UseSwagger();

            app.Run(async (context) =>
            {
                await context.Response
                   .WriteAsync("GeekBurger.Products running");
            });

        }
    }
}
