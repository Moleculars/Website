using Bb.WebHost.Startings;
using Microsoft.OpenApi.Models;
using MudBlazor.Services;

namespace MolecularSite.CommandLines
{

    public class WebSite
    {

        public WebSite(string[] args)
        {
            this._args = args;
        }


        public void RunSite()
        {

            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;


            var builder = WebApplication.CreateBuilder();
            var services = builder.Services;
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();
            services.AddHttpContextAccessor();

            var loader = builder.LoadConfiguration(this._args);

            var useSwagger = loader != null
                && loader.InitialConfiguration != null
                && loader.InitialConfiguration.UseSwagger;


            if (useSwagger)
            {
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
                ;


            if (useSwagger)
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

            app.Run();

        }

        private void CurrentDomain_FirstChanceException(object? sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            if (e.Exception is NullReferenceException ex)
            {

            }
        }

        private readonly string[] _args;

    }

}
