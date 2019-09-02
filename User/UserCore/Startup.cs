using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTracing;
using OpenTracing.Util;
using Polly;
using Polly.Extensions.Http;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using UserCore.Data;
using UserCore.Infrastructure.Middlewares;
using UserCore.Models;
using UserCore.Repositories;

namespace UserCore
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //EF連線字串
            services.AddDbContext<SchoolContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            //依賴注入 將介面設定建構物建
            services.AddScoped<IRepository<Oldwhite, int>, UserRepository>();


            services.AddOpenTracing();

            //jwt service
            ConfigureAuthService(services);

            //Jaeger
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                string serviceName = "My Jaeger Demo";
                Tracer tracer = new Tracer.Builder(serviceName)
                .WithSampler(new ConstSampler(true))
                .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    name: "v1",
                    info: new Info
                    {
                        Title = "MySwaggerTest",
                        Version = "1.0.0",
                        Description = "This is Jason Test",
                        Contact = new Contact
                        {
                            Email = "abc@gmail.com",
                            Name = "Json",
                            Url = "www.xxx.com",
                        },
                        License = new License
                        {
                            Name = "CC SS-SF-WF 4.0",
                            Url = "",
                        },
                    });
                var filePath = Path.Combine(@".\bin\Debug\netcoreapp2.2", "Api.xml");
                c.IncludeXmlComments(filePath);
            });

            //services.AddHttpClient<IBasketService, BasketService>()
            //    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //    .AddPolicyHandler();

            //services.AddHttpClient<IBasketService, BasketService>()
            //    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //    .AddPolicyHandler(GetRetryPolicy())
            //    .AddPolicyHandler(GetCircuitBreakerPolicy());
        }
        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }

        //jwt
        private void ConfigureAuthService(IServiceCollection services)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            string identityUrl = Configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "basket";
            });
        }


        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            //middleware
            app.UseMiddleware<ByPassAuthMiddleware>();

            app.UseAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //middleware
            ConfigureAuth(app);

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                 // url: 需配合 SwaggerDoc 的 name。 "/swagger/{SwaggerDoc name}/swagger.json"
                 url: "/swagger/v1/swagger.json",
                 // description: 用於 Swagger UI 右上角選擇不同版本的 SwaggerDocument 顯示名稱使用。
                 name: "RESTful API v1.0.0"
                     );
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
