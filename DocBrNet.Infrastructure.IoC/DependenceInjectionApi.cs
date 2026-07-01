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

    public static IServiceCollection AddInfrastructureOpenApi(this IServiceCollection services, IConfiguration configuration)
    {
        // O .NET 10 já ativa os comentários XML por Source Generator internamente aqui
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "DocBrNet.API",
                    Version = "v1",
                    Description = "Web API for validating and generating Brazilian identifiers.",
                    Contact = new OpenApiContact
                    {
                        Name = "Misael C. Homem",
                        Url = new Uri(configuration.GetSection("AuthorProfile").Value!)
                    }
                };
                return Task.CompletedTask;
            });
        });

        return services;
    }
}
