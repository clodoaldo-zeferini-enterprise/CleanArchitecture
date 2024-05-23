using Microsoft.OpenApi.Models;

namespace Infrastructure.Base.Configurations.SwaggerConfig
{
    public class Swagger
    {
        public SwaggerDoc SwaggerDoc { get; set; }
        public SwaggerEndpoint SwaggerEndpoint { get; set; }

        public OpenApiInfo OpenApiInfo { get; set; }

        public Swagger()
        {
            SwaggerDoc = new SwaggerDoc("Name", OpenApiInfo, SwaggerEndpoint);
            SwaggerEndpoint = new SwaggerEndpoint();
            OpenApiContact openApiContact = new Microsoft.OpenApi.Models.OpenApiContact();
            openApiContact.Email = "Email";
            openApiContact.Name = "Name";
            openApiContact.Url = new System.Uri("Url");
            OpenApiLicense openApiLicense = new Microsoft.OpenApi.Models.OpenApiLicense();
            openApiLicense.Name = "Name";
            openApiLicense.Url = new System.Uri("Url");

            OpenApiInfo = new OpenApiInfo("Title", "Version", "Description", openApiContact, openApiLicense);

        }
    }
}
