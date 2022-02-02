using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.Translations;
using System.Globalization;

namespace Bb.Translations.Services
{


    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(ITranslateService), LifeCycle = IocScopeEnum.Singleton)]
    public class TranslateService : ITranslateService
    {


        public TranslateService(TranslateServiceDataAccess dataAccess)
        {

            DataAccess = dataAccess;

            _availableCultures = new CultureInfo[]
            {
                CultureInfo.GetCultureInfo("fr-FR"),
                CultureInfo.GetCultureInfo("en-US"),
            };

        }
     

        public TranslateServiceDataAccess DataAccess { get;  }


        public string Translate(TranslatedKeyLabel key)
        {
            return Translate(CultureInfo.CurrentUICulture, key);
        }


        public string Translate(CultureInfo culture, TranslatedKeyLabel key)
        {
            return key.DefaultDisplay;
        }


        public CultureInfo[] AvailableCultures { get { return _availableCultures; } }

        private CultureInfo[] _availableCultures;


    }
}
