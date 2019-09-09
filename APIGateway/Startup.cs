﻿using APIGateway.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace APIGateway
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
            //services.AddMvc(); -- no need MVC // Ocelot 
            services.AddOcelot(Configuration).AddSingletonDefinedAggregator<FakeDefinedAggregator>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } //app.UseMvc(); -- no need MVC // Ocelot 
            app.UseOcelot().Wait();
        }
    }

    //public class Startup
    //{
    //    public Startup(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }

    //    public IConfiguration Configuration { get; }

    //    // This method gets called by the runtime. Use this method to add services to the container.
    //    public void ConfigureServices(IServiceCollection services)
    //    {

    //        //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    //        services.AddOcelot(Configuration);
    //    }

    //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //    public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //    {
    //        if (env.IsDevelopment())
    //        {
    //            app.UseDeveloperExceptionPage();
    //        }
    //        //else
    //        //{
    //        //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //        //    app.UseHsts();
    //        //}

    //        //app.UseHttpsRedirection();
    //        //app.UseMvc();
    //        await app.UseOcelot();
    //    }
//}
}
