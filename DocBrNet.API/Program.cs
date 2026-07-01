/***************************************************************************************
*                                                                                      *
*    E, quando estiverdes orando, perdoai, se tendes alguma coisa contra alguém,       *
*    para que vosso Pai, que está nos céus, vos perdoe as vossas ofensas.              *
*                                                                                      *
*    Marcos 11:25                                                                      *
*                                                                                      *
****************************************************************************************/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInfrastructureApi()
    .AddInfrastructureOpenApi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // 1. Gera o JSON nativo do OpenAPI na rota /openapi/v1.json
    app.MapOpenApi();

    // 2. Ativa a interface visual do Swagger
    app.UseSwaggerUI(options =>
    {
        // Aponta para o JSON gerado pela engine nativa do .NET
        options.SwaggerEndpoint("/openapi/v1.json", "API v1");
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

#region Endpoints

app.MapPost("/cnpj/check", ([FromServices] ICnpjService cnpjService, [FromBody] CnpjCheckerRequestDto request) =>
{
    var result = cnpjService.Check(request);
    var response = new ApiResponse<CnpjResponseDto>(result);
    return Results.Ok(response);
})
.WithName("CheckCnpj")
.WithTags("CNPJ")
.Produces<ApiResponse<CnpjResponseDto>>(StatusCodes.Status200OK)
.AddOpenApiOperationTransformer((opperation, context, ct) =>
{
    opperation.Summary = "Verificar um CNPJ";
    opperation.Description = "Recebe um CNPJ (numérico ou alfanumérico) com ou sem máscara e retorna se é válido.";
    return Task.FromResult(opperation);
});

app.MapPost("cnpj/generator", (
    [FromServices] ICnpjService cnpjService,
    [FromBody] CnpjGeneratorRequestDto request) =>
{
    var result = cnpjService.Generate(request);
    var response = new ApiResponse<IEnumerable<string>>(result);
    return Results.Ok(response);
})
.WithTags("CNPJ")
.Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status200OK)
.AddOpenApiOperationTransformer((opperation, context, ct) => {
    opperation.Summary = "Gerar CNPJ's";
    opperation.Description = "Gera CNPJ's numéricos ou alfanuméricos e com ou sem máscara.";
    return Task.FromResult(opperation);
});

app.MapPost("/cpf/check", ([FromServices] ICpfService cpfService, [FromBody] CpfCheckerRequestDto request) =>
{
    var result = cpfService.Check(request);
    var response = new ApiResponse<CpfResponseDto>(result);
    return Results.Ok(response);
})
.WithName("CheckCpf")
.WithTags("CPF")
.Produces<ApiResponse<CpfResponseDto>>(StatusCodes.Status200OK)
.AddOpenApiOperationTransformer((opperation, context, ct) => {
    opperation.Summary = "Verificar um CPF";
    opperation.Description = "Recebe um CPF com ou sem máscara e retorna se é válido.";
    return Task.FromResult(opperation);
});

app.MapPost("cpf/generator", (
    [FromServices] ICpfService cpfService,
    [FromBody] CpfGeneratorRequestDto request) =>
{
    var result = cpfService.Generate(request);
    var response = new ApiResponse<IEnumerable<string>>(result);
    return Results.Ok(response);
})
.WithTags("CPF")
.Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status200OK)
.AddOpenApiOperationTransformer((opperation, context, ct) => {
    opperation.Summary = "Gerar CPF's";
    opperation.Description = "Gera CPF's com ou sem máscara.";
    return Task.FromResult(opperation);
});

#endregion

await app.RunAsync();
