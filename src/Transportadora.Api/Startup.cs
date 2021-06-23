using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.IO.Compression;
using Transportadora.Api.Configuration;
using Transportadora.Api.Exceptions.Exceptions;
using Transportadora.Data.Context;

namespace Transportadora.Api
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

            services.ResolveDependencies();
            services.AddDbContext<TruckDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["DefaultConnection"]).UseLazyLoadingProxies();
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddAuthentication(IISServerDefaults.AuthenticationScheme);

            services.Configure<IISServerOptions>(options => options.AutomaticAuthentication = false);

            services.Configure<IISOptions>(options => options.ForwardClientCertificate = false);

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }
                );

            services.AddGlobalExceptionHandlerMiddleware();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());


                options.AddPolicy("Production",
                    builder =>
                        builder
                            .WithMethods("GET")
                            .WithOrigins("https://localhost")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
                            .AllowAnyHeader());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHsts();
            app.UseAuthentication();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseResponseCompression();
            app.UseGlobalExceptionHandlerMiddleware();
            app.UseMvc();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
