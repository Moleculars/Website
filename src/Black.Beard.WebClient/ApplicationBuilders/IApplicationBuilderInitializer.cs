using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bb.WebClient.ApplicationBuilders
{


    public interface IApplicationBuilderInitializer
    {

        void Initialize(IServiceCollection services, IConfiguration configuration);

        void Configure(IApplicationBuilder app, IWebHostEnvironment env);

        void Configure(IApplicationBuilder app, IHostEnvironment env);


    }

}
