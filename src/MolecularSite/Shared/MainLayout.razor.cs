using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Reflection;

namespace MolecularSite.Shared
{

    public partial class MainLayout
    {

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        [Inject]
        private UIService? UIService { get; set; }

        [Inject]
        private TranslateService? translateService { get; set; }

        protected override async Task OnInitializedAsync()
        {

            if (translateService ==null)
                translateService = new TranslateService();

            menus = new List<DynamicServerMenu>();
            var menuBuilder = new MenuConverter(translateService);
            if (UIService != null)
            {
                var m = await UIService.GetUI(UIService.TopMenu);
                if (m != null)
                    foreach (var m1 in m)
                        menus.Add((DynamicServerMenu)menuBuilder.Convert(m1));
            }
        }

        bool _drawerOpen = false;
        private List<DynamicServerMenu>? menus;


        //private Task GetRouteUrlWithAuthorizeAttribute()
        //{

        //    // Get all the components whose base class is ComponentBase
        //    var components = Assembly.GetExecutingAssembly()
        //                           .ExportedTypes
        //                           .Where(t =>
        //                          t.IsSubclassOf(typeof(ComponentBase)));

        //    foreach (var component in components)
        //    {
        //        // Print the name (Type) of the Component
        //        Console.WriteLine(component.ToString());

        //        // Now check if this component contains the Authorize attribute
        //        var allAttributes = component.GetCustomAttributes(inherit: true);

        //        var authorizeDataAttributes =
        //                        allAttributes.OfType<IAuthorizeData>().ToArray();

        //        // If it does, show this to us... 
        //        foreach (var authorizeData in authorizeDataAttributes)
        //        {

        //            Console.WriteLine(authorizeData.ToString());
        //        }
        //    }

        //    return Task.CompletedTask;

        //}


    }

    public static class PrerenderRouteHelper
    {

        /// <summary>
        /// Creates a <see cref="PrerenderRouteHelper"/> from the <see cref="RouteAttribute"/>s
        /// decorating components in the provided assembly
        /// </summary>
        public static List<string> GetRoutes(Assembly assembly)
        {
            // Get all the components whose base class is ComponentBase
            var components = assembly
                .ExportedTypes
                .Where(t => t.IsSubclassOf(typeof(ComponentBase)));

            return components
                .Select(component => GetRouteFromComponent(component))
                .Where(config => config is not null)
                .ToList();
        }

        private static string GetRouteFromComponent(Type component)
        {
            var attributes = component.GetCustomAttributes(inherit: true);

            var routeAttribute = attributes.OfType<RouteAttribute>().FirstOrDefault();

            if (routeAttribute is null)
                return String.Empty;

            var route = routeAttribute.Template;

            if (string.IsNullOrEmpty(route))
            {
                throw new Exception($"RouteAttribute in component '{component}' has empty route template");
            }

            // Don't support tokens at this point
            if (route.Contains('{'))
            {
                throw new Exception($"RouteAttribute for component '{component}' contains route values. Route values are invalid for prerendering");
            }

            return route;
        }
    }


}
