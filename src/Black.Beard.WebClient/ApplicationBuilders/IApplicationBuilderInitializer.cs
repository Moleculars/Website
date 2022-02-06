using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bb.WebClient.ApplicationBuilders
{


    public interface IApplicationBuilderInitializer
    {


        bool CanInitialize(WebApplicationBuilder builder);


        void Initialize(WebApplicationBuilder builder);




        bool CanConfigure(IApplicationBuilder app);


        void Configure(IApplicationBuilder app);


    }

}
