using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MolecularSite.Data;
using MudBlazor.Services;
using Bb.WebHost.Startings;
using Microsoft.OpenApi.Models;
using Bb.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();

var loader = builder
    .LoadConfiguration(args)
    ;


if (loader != null 
    && loader.InitialConfiguration != null 
    && loader.InitialConfiguration.UseSwagger)
{

    // Swagger
    builder.Services.AddEndpointsApiExplorer();
    // builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "ToDo API",
            Description = "An ASP.NET Core Web API for managing ToDo items",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Example Contact",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://example.com/license")
            }
        });
    });
}

var app = builder.Build()
    .ConfigureInjectedServices(loader)
    .ConfigureUseExceptionHandler(err => err.UseCustomErrors(loader))
;

if (loader.InitialConfiguration.UseSwagger)
{
    // Swagger 
    app.UseSwagger();
    // app.UseSwaggerUI();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change
    // this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app
        .AppendMiddleware<LoggingSupervisionMiddleware>();

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();