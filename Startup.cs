using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kewlnetcoreapi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace kewlnetcoreapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // setup dependency injection
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"http://localhost:9090/";
                options.Audience = "http://localhost:5000";
            });
            
            services.AddControllers();
            services.AddSingleton<IConferenceService, ConferenceMemoryService>();
            
            // services.AddAuthentication()
            //     .AddOpenIdConnect("oidc", options =>
            //     {
            //         options.Authority = "http://localhost:9090";
            //         options.ClientId = "my-client";
            //         options.ClientSecret = "my-secret";
            //         options.GetClaimsFromUserInfoEndpoint = true;
            //         options.ResponseType = "code id_token";
            //         options.Scope.Clear();
            //         options.Scope.Add("openid");
            //         options.ClaimActions.MapUniqueJsonKey("eduPersonTargetedID", "eduPersonTargetedID");
            //         options.ClaimActions.MapUniqueJsonKey("eduPersonScopedAffiliation", "eduPersonScopedAffiliation");
            //
            //         //   If you plan to map extended attributes, such as username etc:
            //         options.ClaimActions.MapUniqueJsonKey("username", "username");
            //
            //         options.CallbackPath = new PathString("/redirect");
            //     });
        }

        // setup http request pipeline middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // endpoints.MapControllerRoute(
                //     name: "default",
                //     pattern: "{controller=Conference}/{action=Index}/{id?}");
            });
        }
    }
}
