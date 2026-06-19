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
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddInfrastructureApi()
    .AddInfrastructureSwagger(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
.WithOpenApi(op =>
{
    op.Summary = "Verificar um CNPJ";
    op.Description = "Recebe um CNPJ (numérico ou alfanumérico) com ou sem máscara e retorna se é válido.";
    return op;
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
.WithOpenApi(op =>
{
    op.Summary = "Gerar CNPJ's";
    op.Description = "Gera CNPJ's numéricos ou alfanuméricos e com ou sem máscara.";
    return op;
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
.WithOpenApi(op =>
{
    op.Summary = "Verificar um CPF";
    op.Description = "Recebe um CPF com ou sem máscara e retorna se é válido.";
    return op;
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
.WithOpenApi(op =>
{
    op.Summary = "Gerar CPF's";
    op.Description = "Gera CPF's com ou sem máscara.";
    return op;
});

#endregion

await app.RunAsync();
