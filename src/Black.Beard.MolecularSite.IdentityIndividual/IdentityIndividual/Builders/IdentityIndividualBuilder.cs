using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.DataAnnotations;
using Bb.Identity;
using Bb.IdentityIndividual.Services;
using Bb.WebClient.ApplicationBuilders;
using Bb.WebClient.Startings;
using Microsoft.AspNetCore.Authorization;
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

            //services.Add(ServiceDescriptor.Transient(typeof(IRepository<ILogin>), typeof(RepositoryLogin)));
            services.Add(ServiceDescriptor.Transient(typeof(IRepository<IRegisterModel>), typeof(RepositoryRegister)));

            var cnx = _configuration.InitialConnection.GetConnection();
            builder.Services
                .AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(cnx.ConnectionString)
                );

            
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            services
                .AddDefaultIdentity<IdentityUser>(options => 
                    options.SignIn.RequireConfirmedAccount = true
                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;
                options.User.RequireUniqueEmail = true;
            });

            services.Configure<PasswordHasherOptions>(option =>
            {
                option.IterationCount = 1200;
            });

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //    options.LoginPath = "/Home/Login";
            //    options.AccessDeniedPath = "/Account/AccessDenied";
            //    options.SlidingExpiration = true;
            //});

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            //builder.Services.AddAuthorization(options =>
            //{
            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //});

            //services.AddIdentityServer();
            //services.AddDeveloperSigningCredential();
            //services.AddSigningCredential();


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
