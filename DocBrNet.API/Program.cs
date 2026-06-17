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

app.MapPost("/cnpj/validator", ([FromServices] ICNPJService cnpjService, [FromBody] CNPJRequestDto request) =>
{
    var result = cnpjService.Validate(request.Value);
    var response = new ApiResponse<CNPJResponseDto>(result);
    return Results.Ok(response);
})
.WithName("ValidateCnpj")
.WithTags("CNPJ")
.Produces<ApiResponse<CNPJResponseDto>>(StatusCodes.Status200OK)
.WithOpenApi(op =>
{
    op.Summary = "Validade a CNPJ";
    op.Description = "Recebe um CNPJ (numérico ou alfanumérico) com ou sem máscara e retorna se é válido.";
    return op;
});

app.MapPost("/cpf/validator", ([FromServices] ICPFService cpfService, [FromBody] CPFRequestDto request) =>
{
    var result = cpfService.Validate(request.Value);
    var response = new ApiResponse<CPFResponseDto>(result);
    return Results.Ok(response);
})
.WithName("ValidateCpf")
.WithTags("CPF")
.Produces<ApiResponse<CPFResponseDto>>(StatusCodes.Status200OK)
.WithOpenApi(op =>
{
    op.Summary = "Validade a CPF";
    op.Description = "Recebe um CPF com ou sem máscara e retorna se é válido.";
    return op;
});

#endregion

await app.RunAsync();
