using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.WebClient.UIComponents;
using Bb.WebClient.UIComponents.Glyphs;
using Microsoft.AspNetCore.Components.Routing;
using System;

namespace Bb.Translations.Menus
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

            var guidTranslations = UIService.Guids.Translations;

            var home = service.GetMenu(UIService.LeftMenu, this._guidHome);       

            service.GetMenuOrCreate(UIService.LeftMenu, guidTranslations, "p:MenuLeft,k:TranslationMenu,l:en-us,d:Translations")
                .SetAction(NavLinkMatch.Prefix, "/Translations")
                .SetKeyboardArrowDown(false)
                .SetIcon(GlyphFilled.Translate)
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