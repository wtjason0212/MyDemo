using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication1.Tasks;
using EvenBus;
using EvenBus.Abstractions;
using EventBusKafkaMQ;
using MyBackgroundTask.Repositories;
using MyBackgroundTask.Model;
using MyBackgroundTask;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<TestBackgroundTask>(Configuration);
            services.AddOptions();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ProductContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));


            services.AddSingleton<IHostedService, TestBackgroundTask>();

            services.AddSingleton<IProductRespository, ProductRepository>();


            services.AddSingleton<IKafkaMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultKafkaMQPersistentConnection>>();

                return new DefaultKafkaMQPersistentConnection(logger);
            });

            RegisterEventBus(services);

            //create autofac based service provider
            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //if (env.IsDevelopment())
            //{
            //app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseMvc();

        }

        private void RegisterEventBus(IServiceCollection services)
        {
            var subscriptionClientName = Configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusKafka>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<EventBusKafka>>();

                return new EventBusKafka(logger);
            });

            //services.AddSingleton<IRespository<Product>,ProductRepository>(sp =>
            //{
            //    var sss = sp.GetRequiredService<ProductContext>();
            //    return new ProductRepository(sss);
            //}

           //);

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        }
    }
}
