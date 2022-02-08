using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.WebClient.UIComponents;
using Bb.WebClient.UIComponents.Glyphs;

namespace Bb.Configurations.Menus
{

    [ExposeClass(ConstantsCore.Initialization)]
    public class TopMenuRightBuilder : IInjectBuilder<UIService>
    {

        public TopMenuRightBuilder()
        {

        }


        public Type Type => typeof(UIService);


        public object Run(UIService service)
        {

            //var guidLogin = UIService.Guids.Login;

            //var menuLanguages = service.GetMenuOrCreate(UIService.TopRightMenu, guidLogin)
            //    //.SetAction(Microsoft.AspNetCore.Components.Routing.NavLinkMatch.Prefix, typeof(Login))
            //    .SetIcon(GlyphFilled.Login)
                
            //    ;

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