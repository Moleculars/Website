using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.WebClient.ApplicationBuilders;
using Microsoft.AspNetCore.Builder;
using MudBlazor.Services;
using Microsoft.Extensions.DependencyInjection;
using Bb.WebHost.Startings;
using Bb.Middleware;
using Bb.WebClient.Startings;
using Microsoft.Extensions.Hosting;

namespace Bb.WebHost.ApplicationBuilders
{


    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(IApplicationBuilderInitializer), LifeCycle = IocScopeEnum.Transiant)]
    public class BlazorApplicationBuilderInitializer : IApplicationBuilderInitializer
    {


        public bool CanInitialize(WebApplicationBuilder builder)
        {
            return true;
        }


        public void Initialize(WebApplicationBuilder builder)
        {
            var services = builder.Services;
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();
            services.AddHttpContextAccessor();
        }


        public bool CanConfigure(IApplicationBuilder app)
        {
            return true;
        }


        public void Configure(IApplicationBuilder app)
        {

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            var a = app as WebApplication;
            a.MapBlazorHub();
            a.MapFallbackToPage("/_Host");

            // Configure the HTTP request pipeline.
            if (!a.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change
                // this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {

                a.AppendMiddleware<LoggingSupervisionMiddleware>();

            }

            var loader = app.ApplicationServices.GetService(typeof(InitializationLoader)) as InitializationLoader;

            a.ConfigureUseExceptionHandler(err => err.UseCustomErrors(loader));

        }


    }

}
