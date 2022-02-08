using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.WebClient.UIComponents;
using Bb.WebClient.UIComponents.Glyphs;

namespace Bb.Configurations.Menus
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