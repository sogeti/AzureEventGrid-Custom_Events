using EventGrid.Subscriber.AuthorizationPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace EventGrid.Subscriber
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
            services.AddMvc();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            // This requires incoming calls to include a key in the query string (e.g. https://endpoint?key=<yourkey>
            // This can be included in the endpoint URL when registering the subscription in Azure Event Grid
            services.AddAuthorization(options =>
            {
                options.AddPolicy("KeyRequired",
                    policy => policy.Requirements.Add(new KeyRequirement("70EDC39E-D2EB-4C2F-8D2B-81EBF8CFB558")));
                // TODO: you may want to generate a cryptographically random key instead of this GUID, not hardcode it, etc
                // Notice that Event Grid is passing the key as-is, while it is being URL-decoded on the receiving side.
                // So make sure that you specify a URL-encoded key when registering it in Event Grid
            });
            services.AddSingleton<IAuthorizationHandler, KeyHandler>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
