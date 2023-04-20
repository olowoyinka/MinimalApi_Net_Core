using MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.RegisterEndpointDefinitions();

app.Run();