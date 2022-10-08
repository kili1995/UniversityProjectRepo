namespace University.Api.Helpers
{
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "University Api Rest Full",
                Version = description.ApiVersion.ToString(),
                Description = "API for University Project",
                Contact = new OpenApiContact()
                {
                    Email = "jorgecapello1995@gmail.com",
                    Name = "Jorge Capello",
                    Url = new Uri($"https://www.linkedin.com/in/jorgecapello1995")
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += "This API Version has been deprecated.";
            }
            return info;
        }


        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);   
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }
    }
}
