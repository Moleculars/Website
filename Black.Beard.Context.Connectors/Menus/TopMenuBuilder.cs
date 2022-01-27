using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.WebHost.UIComponents;
using Bb.WebHost.UIComponents.Glyphs;
using System;

namespace Bb.Context.Connectors
{

    [ExposeClass(ConstantsCore.Initialization)]
    public class TopMenuBuilder : IInjectBuilder<UIService>
    {

        public TopMenuBuilder()
        {

        }


        public Type Type => typeof(UIService);


        public object Run(UIService service)
        {

            var guidLanguages = UIService.Guids.Languages;

            var menuLanguages = service.GetMenu(UIService.TopMenu, guidLanguages)
                .SetIcon(GlyphFilled.Translate)
                ;

            menuLanguages
                .Add(new UIComponent(null, "::English"))
                .Add(new UIComponent(null, "::French"))
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

    }

}