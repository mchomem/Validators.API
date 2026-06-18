namespace DocBrNet.Core.Application.ProfileMappings;

public static class ProfileMapping
{
    public static void RegisterMappings(TypeAdapterConfig config)
    {
        config.NewConfig<Cnpj, CnpjResponseDto>().TwoWays();
        config.NewConfig<Cpf, CpfResponseDto>().TwoWays();
    }
}
