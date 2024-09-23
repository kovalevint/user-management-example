using backend.Actions;
using backend.Infrastructure.Keycloak;

var builder = WebApplication.CreateBuilder(args);

// Configure Keycloak options
builder.Services.Configure<KeycloakOptions>(
    builder.Configuration.GetSection(KeycloakOptions.SectionName)
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKeycloakHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUsersManagementRoutes();

app.Run();
