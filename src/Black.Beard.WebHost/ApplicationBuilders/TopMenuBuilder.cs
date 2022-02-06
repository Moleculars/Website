using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.Translations;
using Bb.WebClient.UIComponents;
using Bb.WebClient.UIComponents.Glyphs;
using System;

namespace Bb.WebHost.ApplicationBuilders
{

    [ExposeClass(ConstantsCore.Initialization)]
    public class TopMenuBuilder : IInjectBuilder<UIService>
    {

        public TopMenuBuilder(ITranslateService translateService)
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
            {

                var display = string.Empty;
                if (item.IetfLanguageTag == "en-US")
                    display = $"p:menuLanguage, k:{item.DisplayName},l:en-us, d:language {item.EnglishName}";
                else
                    display = $"p:menuLanguage, k:{item.DisplayName},l:en-us, d:language {item.EnglishName}, l1:{item.IetfLanguageTag},d1:{item.NativeName}";
                var d = (TranslatedKeyLabel)display;
                menuLanguages.Add(new UIComponent(null, display));
            }
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

        private readonly ITranslateService _translateService;


    }

}