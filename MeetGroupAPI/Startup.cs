using MeetGroupAPI.Data.Repositories;
using MeetGroupAPI.Models;
using MeetGroupAPI.Models.Interfaces;
using MeetGroupAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MeetGroupAPI
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

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });


            var signingConfigurations = new SigningHSConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                    Configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions => {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    

            }).AddJwtBearer(bearerOptions =>
            {
                var tokenValidationParameters = bearerOptions.TokenValidationParameters;
                tokenValidationParameters.ValidateIssuerSigningKey = true;
                tokenValidationParameters.ValidateIssuer = false;
                tokenValidationParameters.ValidIssuer = "http://localhost:5000/";
                tokenValidationParameters.IssuerSigningKey = signingConfigurations.Key;

                tokenValidationParameters.ValidateAudience = false; /*true*/
                tokenValidationParameters.ValidateLifetime = true;
                tokenValidationParameters.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth => {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            // Swagger -->
            services.AddSwaggerGen(c =>
            {
                
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Meet Group",
                    Description = "Api Rest AspNetCore 3.1",
                    Contact = new OpenApiContact
                    {
                        Name = "Meet Group",
                    },
                    License = new OpenApiLicense
                    {
                        Name = DateTime.UtcNow.ToString("yyyy") + " MeetGroup | Todos os direitos reservados.",

                    }

                });  
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });

                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
             
                });
            
                services.AddControllers();
                services.AddScoped<ApiDbContext>();
                services.AddTransient<IUsuarioInterface, UsuarioRepository>();
                services.AddTransient<IAgendamentoInterface, AgendamentoRepository>();
                services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeetGroup API");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
