using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.Translations;
using Bb.WebClient.UIComponents;
using Bb.WebClient.UIComponents.Glyphs;
using System;

namespace Bb.WebHost.ApplicationBuilders
{

    [ExposeClass(ConstantsCore.Initialization)]
    public class TopMenuLeftBuilder : IInjectBuilder<UIService>
    {

        public TopMenuLeftBuilder(ITranslateService translateService)
        {
            _translateService = translateService;
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

        private readonly ITranslateService _translateService;


    }

}