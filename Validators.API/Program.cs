/***************************************************************************************
*                                                                                      *
*    E, quando estiverdes orando, perdoai, se tendes alguma coisa contra algu�m,       *
*    para que vosso Pai, que est� nos c�us, vos perdoe as vossas ofensas.              *
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

app.MapGet("/cnpj/validator", ([FromServices] ICNPJService cnpjService, [FromQuery] string cnpj) =>
{
    var result = cnpjService.Validate(cnpj);
    var response = new ApiResponse<CNPJDto>(result);
    return Results.Ok(response);
})
.WithName("ValidateCnpj")
.WithTags("CNPJ")
.Produces<ApiResponse<CNPJDto>>(StatusCodes.Status200OK)
.WithOpenApi(op =>
{
    op.Summary = "Validade a CNPJ";
    op.Description = "Recebe um CNPJ (num�rico ou alfanum�rico) com ou sem m�scara e retorna se � v�lido.";
    return op;
});

app.MapGet("/cpf/validator", ([FromServices] ICPFService cpfService, [FromQuery] string cpf = "280.012.389-38") =>
{
    var result = cpfService.Validate(cpf);
    var response = new ApiResponse<CPFDto>(result);
    return Results.Ok(response);
})
.WithName("ValidateCpf")
.WithTags("CPF")
.Produces<ApiResponse<CPFDto>>(StatusCodes.Status200OK)
.WithOpenApi(op =>
{
    op.Summary = "Validade a CPF";
    op.Description = "Recebe um CPF com ou sem m�scara e retorna se � v�lido.";
    return op;
});

#endregion

await app.RunAsync();
