using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.IdentityIndividual.Pages;
using Bb.Services;
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

            var guidMenuUser = UIService.Guids.MenuUser;
            var guidRegister = UIService.Guids.Register;
            var guidLogin = UIService.Guids.Login;

            var menuUser = (UIComponentMenu)service.GetMenuOrCreate(UIService.TopRightMenu, guidMenuUser)
                    .SetIcon(GlyphFilled.VerifiedUser)
                ;

            menuUser.AddMenu(guidLogin, "p:SecurityMenu,k:MenuMogin,l:en-us,d:Login")
                    .SetViewGuard<GuardMenuIdentity>(c => !c.IsIdentified())
                    .SetAction(Microsoft.AspNetCore.Components.Routing.NavLinkMatch.Prefix, typeof(Login))
                    .SetIcon(GlyphFilled.SupervisedUserCircle)
                ;

            menuUser.AddMenu(guidRegister, "p:SecurityMenu,k:MenuRegister,l:en-us,d:Register")
                    .SetViewGuard<GuardMenuIdentity>(c => !c.IsIdentified())
                    .SetAction(Microsoft.AspNetCore.Components.Routing.NavLinkMatch.Prefix, typeof(Register))
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

    }

}