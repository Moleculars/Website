using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.WebClient.UIComponents;
using Bb.WebClient.UIComponents.Glyphs;
using Microsoft.AspNetCore.Components.Routing;
using System;

namespace Bb.WebHost.ApplicationBuilders
{

    [ExposeClass(ConstantsCore.Initialization)]
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

            service.GetMenuOrCreate(UIService.LeftMenu, guidHome, "p:LeftMenu,k:Home,l:en-us, d:Home")
                .SetActionMatchAll()
                .SetIcon(GlyphFilled.Home)
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


        
        static readonly Guid guidConnectors = new("{C8063B0B-B057-4BCB-8629-19D149FE9881}");

    }

}