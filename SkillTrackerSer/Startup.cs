using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using SkillTrackerSer.ActionFilters;
using SkillTrackerSer.Repositories;
using SkilltrackerSer.BusinessLayer;
using SkillTrackerSer.ActionFilters;
using Microsoft.AspNetCore.Http;

namespace SkillTrackerSer
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
            services.AddCors(Options => {
                Options.AddPolicy("SkillTrackerCORSPolicy", 
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithHeaders());
            });

            var accessKey = Environment.GetEnvironmentVariable("ACCESS_KEY");
            var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");

            var credentials = new BasicAWSCredentials(accessKey,secretKey);
            var config = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.USEast1
            };

            var client = new AmazonDynamoDBClient(credentials,config);
            services.AddSingleton<IAmazonDynamoDB>(client);
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

            services.AddScoped<ISkillTrackerRepository, SkillTrackerRepository>();
            services.AddScoped<ISkillTrackerBusiness, SkillTrackerBusiness>();

           
            services.AddMvc();
            services.AddRouting();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseCors("SkillTrackerCORSPolicy");
            app.Use(async (context, next) =>
            {

                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    return Task.FromResult(0);
                });

                await next();
            });
           


        }
    }
}
