using Bb.ComponentModel;
using Bb.WebClient.Startings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Bb.WebHost.Startings
{

    public static class WebApplicationInitializerInitializer
    {

        /// <summary>
        ///     Adds a middleware to the pipeline that will catch exceptions, log them, and re-execute
        ///     the request in an alternate pipeline. The request will not be re-executed if
        ///     the response has already started.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static WebApplication ConfigureUseExceptionHandler(this WebApplication self, Action<IApplicationBuilder> configure)
        {
            self.UseExceptionHandler(configure);
            return self;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="_initializationLoader"></param>
        /// <returns></returns>
        public static WebApplication ConfigureInjectedServices(this WebApplication app, InitializationLoader _initializationLoader)
        {

            InitializeBuilders(_initializationLoader, app);

            IWebHostEnvironment env = app.Environment;

            foreach (var item in _initializationLoader.InstancesBuilders)
                item.Configure(app, env);

            return app;

        }

        private static void InitializeBuilders(InitializationLoader _initializationLoader, IApplicationBuilder app)
        {

            List<TypeToInject> items = _initializationLoader.InjectBuilders
                .Where(c => typeof(IInjectBuilder).IsAssignableFrom(c.Type))
                .ToList();

            var max = items.Count;
            int count = 0;

            var toRemove = new List<TypeToInject>();

            while (items.Count > 0 && count < max)
            {

                foreach (var item in items)
                {

                    var service = (IInjectBuilder)app.ApplicationServices.GetService(item.ImplementationType);
                    var context = app.ApplicationServices.GetService(service.Type);

                    if (service.CanExecute(context))
                    {
                        service.Run(context);
                        toRemove.Add(item);
                    }

                }

                foreach (var item in toRemove)
                    items.Remove(item);

                toRemove.Clear();

            }

        }


        /// <summary>
        /// Adds a middleware type to the application's request pipeline.
        /// </summary>
        /// <typeparam name="TMiddleware">The middleware type.</typeparam>
        /// <param name="app">The Microsoft.AspNetCore.Builder.IApplicationBuilder instance.</param>
        /// <param name="args">The arguments to pass to the middleware type instance's constructor.</param>
        /// <returns>The Microsoft.AspNetCore.Builder.IApplicationBuilder instance.</returns>
        public static WebApplication AppendMiddleware<TMiddleware>(this WebApplication app, params object[] args)
        {
            app.UseMiddleware<TMiddleware>(args);
            return app;
        }

    }


}
