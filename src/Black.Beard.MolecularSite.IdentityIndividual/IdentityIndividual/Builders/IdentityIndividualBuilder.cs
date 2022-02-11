using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Identity;
using Bb.IdentityIndividual.Services;
using Bb.WebClient.ApplicationBuilders;
using Bb.WebClient.Startings;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Hosting.Internal;

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
                {

                    options.UseSqlServer(cnx.ConnectionString);

                    // Configuration.GetSection(nameof(AppSecrets)).Get<AppSecrets>().ConnectionStrings.DbConnectionString
                    //options.UseSqlServer(cnx.ConnectionString,
                    //        x => x.MigrationsAssembly("DataAccess")
                    //            .EnableRetryOnFailure(maxRetryCount: 3))
                    //            .UseLazyLoadingProxies()
                    //            .EnableSensitiveDataLogging(CurrentEnvironment.IsDevelopment())
                    //            .EnableServiceProviderCaching()
                    //            .UseLoggerFactory(ApplicationDbContext.PropertyAppLoggerFactory);

                });


            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                ;

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

            IServiceScopeFactory scopeFactory = (IServiceScopeFactory)app.ApplicationServices.GetService(typeof(IServiceScopeFactory));

            using (var scope = scopeFactory.CreateScope())
            {

                var ctx = (ApplicationDbContext)scope.ServiceProvider.GetRequiredService(typeof(ApplicationDbContext));

                if (!ctx.Database.EnsureCreated())
                    CreateDatabase(ctx);

                //var migrator = ctx.GetInfrastructure().GetService<IMigrator>();
                //migrator.Migrate();
                //ctx.Database.Migrate();

            }

            app.UseAuthentication();
            app.UseAuthorization();

        }

        //public static void EnsureDatabaseCreated()
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder();
        //    if (HostingEnvironment.IsDevelopment()) optionsBuilder.UseSqlServer(Configuration["Data:dev:DataContext"]);
        //    else if (HostingEnvironment.IsStaging()) optionsBuilder.UseSqlServer(Configuration["Data:staging:DataContext"]);
        //    else if (HostingEnvironment.IsProduction()) optionsBuilder.UseSqlServer(Configuration["Data:live:DataContext"]);
        //    var context = new ApplicationDbContext(optionsBuilder.Options);
        //    context.Database.EnsureCreated();

        //    optionsBuilder = new DbContextOptionsBuilder();
        //    if (HostingEnvironment.IsDevelopment()) optionsBuilder.UseSqlServer(Configuration["Data:dev:TransientContext"]);
        //    else if (HostingEnvironment.IsStaging()) optionsBuilder.UseSqlServer(Configuration["Data:staging:TransientContext"]);
        //    else if (HostingEnvironment.IsProduction()) optionsBuilder.UseSqlServer(Configuration["Data:live:TransientContext"]);
        //    new TransientContext(optionsBuilder.Options).Database.EnsureCreated();
        //}

        private static void CreateDatabase(ApplicationDbContext ctx)
        {

            ctx.Database.OpenConnection();
            ctx.Database.CloseConnection();

            var commandText = ctx.Database.GenerateCreateScript();
            var scripts = commandText.Split(new string[] { "GO", "go", "Go", "gO" }, StringSplitOptions.RemoveEmptyEntries);

            using (Microsoft.Data.SqlClient.SqlConnection cnx = (Microsoft.Data.SqlClient.SqlConnection)ctx.Database.GetDbConnection())
            {
                cnx.Open();
                using (var transaction = cnx.BeginTransaction())
                {

                    try
                    {

                        foreach (var script in scripts)
                            using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(script, cnx))
                            {
                                cmd.Transaction = transaction;

                                var i = cmd.ExecuteNonQuery();

                            }

                        transaction.Commit();

                    }
                    catch (Exception e)
                    {

                        if (System.Diagnostics.Debugger.IsAttached)
                            System.Diagnostics.Debugger.Launch();

                        transaction.Rollback();

                    }
                }

            }
        }

        private readonly InitializationLoader _configuration;

}

}
