using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace $safeprojectname$.DependencyRegistration
{
    public static class Swagger
    {
        private const string EXAMPLE_DOT_COM = "https://example.com";

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSwaggerGen(options =>
            {
                options.DocumentFilter<LowercaseDocumentFilter>();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CAB Portal REST API",
                    Description = "Rest API for accessing CAB Portal data",
                    TermsOfService = new Uri(EXAMPLE_DOT_COM),
                    Contact = new OpenApiContact
                    {
                        Name = "Rawae API",
                        Url = new Uri(EXAMPLE_DOT_COM)
                    },
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                        new OpenApiSecurityScheme
                        {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                        },
                        Array.Empty<string>()
                     }
                 });
            });
        }

        public class LowercaseDocumentFilter : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                var paths = swaggerDoc.Paths;

                var newPaths = new Dictionary<string, OpenApiPathItem>();
                var removeKeys = new List<string>();
                foreach (var path in paths)
                {
                    var newKey = path.Key.ToLower();
                    if (newKey != path.Key)
                    {
                        removeKeys.Add(path.Key);
                        newPaths.Add(newKey, path.Value);
                    }
                }

                foreach (var path in newPaths)
                {
                    swaggerDoc.Paths.Add(path.Key, path.Value);
                }

                foreach (var key in removeKeys)
                {
                    swaggerDoc.Paths.Remove(key);
                }
            }
        }
    }
}
