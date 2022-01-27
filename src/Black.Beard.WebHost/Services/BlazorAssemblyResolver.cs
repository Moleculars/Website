using Bb.ComponentModel;
using System;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Bb.WebHost.Startings;
using Bb.WebClient.Startings;

namespace Bb.Services
{
    
    public class BlazorAssemblyResolver
    {


        public BlazorAssemblyResolver(InitializationLoader loader)
        {

            this._loader = loader;
            var assemblies = new HashSet<Assembly>();

            this._pages = TypeDiscovery.Instance.GetTypesWithAttributes(typeof(RouteAttribute)).ToArray();



            foreach (var item in _pages)
                assemblies.Add(item.Assembly);

            this._assemblies = assemblies.ToArray();

            Instance = this;

        }

        public static BlazorAssemblyResolver Instance { get; private set; }

        /// <summary>
        /// Return the list of assemblies that contains blazor embedded content.
        /// </summary>
        /// <returns></returns>
        public Assembly[] GetAssemblies()
        {
            return this._assemblies;
        }

        /// <summary>
        /// Return the list of assemblies that contains blazor embedded content.
        /// </summary>
        /// <returns></returns>
        public Type[] GetPages()
        {
            return this._pages;
        }

        private readonly Assembly[] _assemblies;
        private readonly Type[] _pages;
        private readonly InitializationLoader _loader;

    }


}
