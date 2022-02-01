using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.WebClient.UIComponents;
using Bb.WebClient.UIComponents.Glyphs;
using System;

namespace Bb.WebHost.ApplicationBuilders
{

    [ExposeClass(ConstantsCore.Initialization)]
    public class TopMenuBuilder : IInjectBuilder<UIService>
    {

        public TopMenuBuilder(TranslateService translateService)
        {
            _translateService = translateService;
        }


        public Type Type => typeof(UIService);


        public object Run(UIService service)
        {

            var guidLanguages = UIService.Guids.Languages;

            var menuLanguages = service.GetMenuOrCreate(UIService.TopMenu, guidLanguages)
                .SetIcon(GlyphFilled.Translate)
                ;

            foreach (var item in _translateService.AvailableCultures)
                menuLanguages.Add(new UIComponent(null, "l:en-us, d:" + item.EnglishName));

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

        private readonly TranslateService _translateService;


    }

}