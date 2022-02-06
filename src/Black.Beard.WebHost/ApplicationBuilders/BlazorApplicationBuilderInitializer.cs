using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.WebClient.ApplicationBuilders;
using Microsoft.AspNetCore.Builder;
using MudBlazor.Services;
using Microsoft.Extensions.DependencyInjection;
using Bb.WebHost.Startings;
using Bb.Middleware;
using Bb.WebClient.Startings;

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


            var loader = app.ApplicationServices.GetService(typeof(InitializationLoader)) as InitializationLoader;

            a.ConfigureUseExceptionHandler(err => err.UseCustomErrors(loader));

        }


    }

}
