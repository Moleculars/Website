using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.Services;
using Bb.WebClient.UIComponents;
using Bb.WebClient.UIComponents.Glyphs;
using Microsoft.AspNetCore.Components.Routing;
using System;

namespace Bb.Configurations.Menus
{

    [ExposeClass(ConstantsCore.Initialization)]
    public class LeftMenuBuilder : IInjectBuilder<UIService>
    {

        public LeftMenuBuilder()
        {
            this._guidHome = UIService.Guids.Home;

        }

        public Type Type => typeof(UIService);

        public object Run(UIService service)
        {

            var guidConfigurations = UIService.Guids.Configurations;

            var home = service.GetMenu(UIService.LeftMenu, this._guidHome);

            service.GetMenuOrCreate(UIService.LeftMenu, guidConfigurations, "p:MenuTop,k:ConfigurationMenu,l:en-us,d:Configuration")
                .SetEnabledGuard<GuardMenuIdentity>(c => c.IsIdentified())
                .SetAction(NavLinkMatch.Prefix, typeof(Pages.ConfigurationComponentView))
                .SetKeyboardArrowDown(false)
                .SetIcon(GlyphOutlined.Settings)
                ;

            return 0;

        }

        public object Run(object context)
        {
            return Run((UIService)context);
        }

        public bool CanExecute(object context)
        {
            return CanExecute((UIService)context);
        }

        public bool CanExecute(UIService service)
        {
            return  service.GetMenu(UIService.LeftMenu, this._guidHome) != null;
            ;
        }
                
        static readonly Guid guidConnectors = new("{C8063B0B-B057-4BCB-8629-19D149FE9881}");
        private readonly Guid _guidHome;

    }

}