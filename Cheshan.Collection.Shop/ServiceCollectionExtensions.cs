using Microsoft.AspNetCore.Authentication.OAuth;

namespace Cheshan.Collection.Shop
{
    public static class ServiceCollectionExtensions
    {
        //public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration config)
        //{
        //    var authOptions = new AuthOptions();
        //    config.Bind(AuthOptions.SectionName, authOptions);

        //    services.AddSwaggerGen(x =>
        //    {
        //        var xmlFile = $"{Assembly.GetAssembly(typeof(MileagesController))?.GetName().Name}.xml";
        //        var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //        if (File.Exists(xmlCommentsPath))
        //        {
        //            x.IncludeXmlComments(xmlCommentsPath, includeControllerXmlComments: true);
        //        }
        //        x.CustomSchemaIds(t => t.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ?? t.Name);
        //        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //        {
        //            Name = JwtBearerDefaults.AuthenticationScheme,
        //            Type = SecuritySchemeType.OAuth2,
        //            Scheme = JwtBearerDefaults.AuthenticationScheme,
        //            BearerFormat = "JWT",
        //            In = ParameterLocation.Header,
        //            Reference = new OpenApiReference
        //            {
        //                Id = JwtBearerDefaults.AuthenticationScheme,
        //                Type = ReferenceType.SecurityScheme
        //            },
        //            Flows = new OpenApiOAuthFlows
        //            {
        //                Password = new OpenApiOAuthFlow
        //                {
        //                    AuthorizationUrl = new Uri(authOptions.Url),
        //                    TokenUrl = new Uri(authOptions.Url),
        //                },

        //            },
        //        });
        //        x.AddSecurityRequirement(new OpenApiSecurityRequirement
        //        {
        //            {
        //                new OpenApiSecurityScheme
        //                {
        //                    Name = JwtBearerDefaults.AuthenticationScheme,
        //                    In = ParameterLocation.Header,
        //                    Scheme = "oauth2",
        //                    Reference = new OpenApiReference
        //                    {
        //                        Id = JwtBearerDefaults.AuthenticationScheme,
        //                        Type = ReferenceType.SecurityScheme
        //                    },
        //                },
        //                new List<string>()
        //            }
        //        });
        //    });

        //    return services;
        //}

    }
}
