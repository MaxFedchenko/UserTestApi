using UserTestApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.Configure(builder.Environment);

app.Run();
