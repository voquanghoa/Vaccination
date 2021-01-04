using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AspCoreAPIStarter.Startup
{
    public static class SwaggerConfig
    {
        public static void ConfigSwagger(this SwaggerGenOptions genOption)
        {
            genOption.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Quản lý quan hệ",
                Description = "API Quản lý quan hệ",
                Contact = new OpenApiContact()
                {
                    Email = "voquanghoa@gmail.com",
                    Name = "Vo Quang Hoa"
                },
                Version = "v1"
            });
            genOption.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            genOption.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            genOption.IncludeXmlComments(xmlPath);
        }
        public static void ConfigSwagger(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(x =>
            {
                x.ConfigObject.Urls = new[]
                {
                    new UrlDescriptor
                    {
                        Name = "Localhost",
                        Url = "v1/swagger.json"
                    }
                };

            });
            app.UseSwagger();
        }
    }
}