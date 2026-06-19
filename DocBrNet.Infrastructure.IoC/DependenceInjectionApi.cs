namespace DocBrNet.Infrastructure.IoC;

public static class DependenceInjectionApi
{
    public static IServiceCollection AddInfrastructureApi(this IServiceCollection services)
    {
        #region Services

        services.AddScoped<ICnpjService, CnpjService>();
        services.AddScoped<ICpfService, CpfService>();

        #endregion

        #region Validators

        services.AddScoped<IValidator<CnpjCheckerRequestDto>, CnpjCheckerRequestValidator>();
        services.AddScoped<IValidator<CpfCheckerRequestDto>, CpfCheckerRequestValidator>();

        services.AddScoped<IValidator<CnpjGeneratorRequestDto>, CnpjGeneratorRequestValidator>();
        services.AddScoped<IValidator<CpfGeneratorRequestDto>, CpfGeneratorRequestValidator>();

        #endregion

        #region Mapster

        var config = TypeAdapterConfig.GlobalSettings;
        ProfileMapping.RegisterMappings(config);
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddMapster();

        #endregion

        return services;
    }

    public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1"
                , new OpenApiInfo
                {
                    Title = "DocBrNet.API",
                    Version = "v1",
                    Description = "Web API for validating and generating Brazilian identifiers.",
                    Contact = new OpenApiContact
                    {
                        Name = "Misael C. Homem",
                        Email = "misael.homem@gmail.com",
                        Url = new Uri(configuration.GetSection("AuthorProfile").Value!)
                    },
                });

            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}
