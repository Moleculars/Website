using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.IdentityIndividual.Services;
using Bb.WebClient.ApplicationBuilders;
using Bb.WebClient.Startings;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bb.IdentityIndividual.Builders
{


    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(IApplicationBuilderInitializer), LifeCycle = IocScopeEnum.Transiant)]
    public class IdentityIndividualBuilder : IApplicationBuilderInitializer
    {

        public IdentityIndividualBuilder(InitializationLoader configuration)
        {
            this._configuration = configuration;
        }


        public bool CanInitialize(WebApplicationBuilder builder)
        {
            return true;
        }


        public void Initialize(WebApplicationBuilder builder)
        {

            var services = builder.Services;

            //services
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            var cnx = _configuration.InitialConnection.GetConnection();
            builder.Services
                .AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(cnx.ConnectionString)
                );

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            
            builder.Services
                .AddDefaultIdentity<IdentityUser>(options => 

                    options.SignIn.RequireConfirmedAccount = true

                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            // services.AddHttpContextAccessor();

        }



        public bool CanConfigure(IApplicationBuilder app)
        {
            return true;
        }


        public void Configure(IApplicationBuilder app)
        {

            app.UseAuthentication();
            app.UseAuthorization();

        }

        private readonly InitializationLoader _configuration;

    }

}
