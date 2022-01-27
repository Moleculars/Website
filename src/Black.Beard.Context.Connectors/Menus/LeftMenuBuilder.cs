using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.WebHost.UIComponents;
using Bb.WebHost.UIComponents.Glyphs;
using Microsoft.AspNetCore.Components.Routing;
using System;

namespace Bb.Context.Connectors
{

    [ExposeClass(ConstantsCore.Initialization)]
    // [InjectorPolicy(typeof(UIService), IocScopeEnum.Singleton)]
    public class LeftMenuBuilder : IInjectBuilder<UIService>
    {

        public LeftMenuBuilder()
        {

        }

        public Type Type => typeof(UIService);

        public object Run(UIService service)
        {

            var guidHome = UIService.Guids.Home;
            var guidConfigurations = UIService.Guids.Configurations;

            service.GetMenu(UIService.LeftMenu, guidHome, "::Home")
                .SetActionMatchAll()
                .SetIcon(GlyphFilled.Home)
                ;

            service.GetMenu(UIService.LeftMenu, guidHome, "::Counter")
                .SetAction(NavLinkMatch.Prefix, "/counter")
                .SetIcon(GlyphFilled.Add)
                ;

            service.GetMenu(UIService.LeftMenu, guidHome, "::Fetch data")
                .SetAction(NavLinkMatch.Prefix, "/fetchdata")
                .SetIcon(GlyphFilled.Add)
                ;

            service.GetMenu(UIService.LeftMenu, guidConfigurations, "::Configuration")
                .SetAction(NavLinkMatch.Prefix, "/configurations")
                .SetKeyboardArrowDown(false)
                .SetIcon(GlyphFilled.PanTool)
                ;

            service.GetMenu(UIService.LeftMenu, guidConnectors, "::Connectors")
                .SetAction(NavLinkMatch.Prefix, "")
                .SetKeyboardArrowDown(false)
                .SetIcon(GlyphFilled.PanTool)
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
            return true;
        }


        
        static Guid guidConnectors = new Guid("{C8063B0B-B057-4BCB-8629-19D149FE9881}");

    }

}