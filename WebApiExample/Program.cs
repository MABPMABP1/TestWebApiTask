using WebApiExample.ExceptionFilters;
using WebApiExample.Services.CredentialsServices;
using WebApiExample.Services.DataServices;
using WebApiExample.Services.TokenServices;
using WebApiExample.Services.TokenServices.TokenManagers;
using WebApiExample.Services.TokenServices.TokenProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICredentialsService, AppSettingsBasedCredentialsService>();
builder.Services.AddScoped<ITokensService, TokensService>();
builder.Services.AddScoped<ITokenProvider, BasicTokenProvider>();
builder.Services.AddSingleton<ITokenManager, InMemoryTokenManager>();
builder.Services.AddScoped<IDataService, BasicDataService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CommonExceptionFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//TODO: not sure if it neccessary with custom authorization implemetation
app.UseAuthorization();

app.MapControllers();

app.Run();
