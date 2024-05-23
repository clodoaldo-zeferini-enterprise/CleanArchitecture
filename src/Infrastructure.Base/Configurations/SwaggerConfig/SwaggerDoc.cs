namespace Infrastructure.Base.Configurations.SwaggerConfig
{
    public class SwaggerDoc
    {
        public string Name { get; set; }
        public OpenApiInfo OpenApiInfo { get; }
        public SwaggerEndpoint SwaggerEndpoint { get; }

        public SwaggerDoc(string name, OpenApiInfo openApiInfo, SwaggerEndpoint swaggerEndpoint)
        {
            Name = name;
            OpenApiInfo = openApiInfo;
            SwaggerEndpoint = swaggerEndpoint;
        }
    }
}
