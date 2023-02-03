namespace CompanyApplicationAPI.Configurations;

public static class CorsConfiguration
{
    public static void AddCors(this IApplicationBuilder app)
    {
        app.UseCors("default");
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options=>
        {
            options.AddPolicy("default",
                builder =>
                {
                    builder
                          .WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
                });
        });
    }
}
