using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using System.Globalization;

namespace Bb.WebHost.UIComponents
{


    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(TranslateService), LifeCycle = IocScopeEnum.Singleton)]
    public class TranslateService
    {

        public TranslateService()
        {

        }

        public string Translate(CultureInfo culture, TranslatedKeyLabel key)
        {

            return key.DefaultDisplay;

        }


    }
}
