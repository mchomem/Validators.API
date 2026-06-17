namespace DocBrNet.Core.Application.ProfileMappings;

public static class ProfileMapping
{
    public static void RegisterMappings(TypeAdapterConfig config)
    {
        config.NewConfig<CNPJ, CNPJResponseDto>().TwoWays();
        config.NewConfig<CPF, CPFResponseDto>().TwoWays();
    }
}
