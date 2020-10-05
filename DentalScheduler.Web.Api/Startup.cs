using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DentalScheduler.Config.DI;
using DentalScheduler.Infrastructure.Common.Persistence;
using DentalScheduler.Dto.Output;
using DentalScheduler.Entities.Identity;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;

namespace DentalScheduler.Web.Api
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
            services.RegisterDependencies();

            services.AddTransient(typeof(Lazy<>), typeof(Lazy<>));
            
            services.AddControllers();

            services.AddDbContext<DentalSchedulerDbContext>(opt => 
                    opt.UseNpgsql(Configuration.GetConnectionString("DentalSchedulerDbConnection"),
                            x => x.MigrationsAssembly("DentalScheduler.Infrastructure"))
                );

            // Add Identity
            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;

                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<DentalSchedulerDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            // Add JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddAuthorization();

            services.AddOData();
            
            // services.AddMvc();
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
                
                c.SwaggerDoc("v1", 
                    new OpenApiInfo 
                    { 
                        Title = "Dental Scheduler",
                        Version = "v1" 
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors(policy => policy.WithOrigins(new string[] { "https://localhost:5001" })
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.EnableDependencyInjection();
                endpoints.MapControllers();
                endpoints.Select().Filter().Expand().OrderBy().Count();
                endpoints.MapODataRoute(routeName: "odata", routePrefix: "odata", model: GetEdmModel());
            });
        }

        private IEdmModel GetEdmModel()
        {
            ODataModelBuilder odataBuilder = new ODataConventionModelBuilder();

            odataBuilder.EntitySet<RoomOutput>("Room")
                .EntityType.HasKey(e => e.ReferenceId);

            odataBuilder.EntitySet<DentalTeamOutput>("DentalTeam")
                .EntityType.HasKey(e => e.ReferenceId);

            var ts = odataBuilder.EntitySet<TreatmentSessionOutput>("TreatmentSession")
                .EntityType.HasKey(e => e.ReferenceId);

            odataBuilder.EntitySet<TreatmentOutput>("Treatment")
                .EntityType.HasKey(e => e.ReferenceId);

            return odataBuilder.GetEdmModel();
        }
    }
}
