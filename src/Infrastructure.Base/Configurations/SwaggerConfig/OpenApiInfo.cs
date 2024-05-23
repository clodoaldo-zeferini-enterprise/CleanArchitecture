using Microsoft.OpenApi.Models;

namespace Infrastructure.Base.Configurations.SwaggerConfig
{
    public class OpenApiInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set;}
        public OpenApiContact Contact { get; set; }
        public OpenApiLicense License { get; set; }
        public OpenApiInfo(string title, string version, string description, OpenApiContact contact, OpenApiLicense license)
        {
            Title = title;
            Version = version;
            Description = description;
            Contact = contact;
            License = license;
        }
    }
}
