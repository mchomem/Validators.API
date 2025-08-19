namespace Validators.Core.Application.ProfileMappings;

public static class ProfileMapping
{
    public static void RegisterMappings(TypeAdapterConfig config)
    {
        config.NewConfig<CNPJ, CNPJDto>().TwoWays();
        config.NewConfig<CPF, CPFDto>().TwoWays();
    }
}
