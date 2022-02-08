using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.Translations;
using Bb.Pages;
using Bb.Services;
using Bb.WebClient.UIComponents;
using Bb.WebClient.UIComponents.Glyphs;
using System;

namespace Bb.WebHost.ApplicationBuilders
{

    [ExposeClass(ConstantsCore.Initialization)]
    public class TopMenuRightBuilder : IInjectBuilder<UIService>
    {

        public TopMenuRightBuilder(ITranslateService translateService)
        {
            _translateService = translateService;
        }


        public Type Type => typeof(UIService);


        public object Run(UIService service)
        {

            var guidLanguages = UIService.Guids.Languages;

            var menuLanguages = service.GetMenuOrCreate(UIService.TopRightMenu, guidLanguages)
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

            var guidLogin = UIService.Guids.Login;

            var menuLogin = service.GetMenuOrCreate(UIService.TopRightMenu, guidLogin)
                .SetViewGuard<GuardMenuIdentity>(c => !c.IsIdentified())
                .SetAction(Microsoft.AspNetCore.Components.Routing.NavLinkMatch.Prefix, typeof(Login))
                .SetIcon(GlyphFilled.SupervisedUserCircle)
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

        private readonly ITranslateService _translateService;


    }

}