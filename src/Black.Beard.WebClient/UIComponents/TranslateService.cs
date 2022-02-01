using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using System.Globalization;

namespace Bb.WebClient.UIComponents
{

    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(TranslateService), LifeCycle = IocScopeEnum.Singleton)]
    public class TranslateService
    {


        public TranslateService(ITranslateServiceDataAccess dataAccess)
        {

            this._dataAccess = dataAccess;
            _availableCultures = new CultureInfo[]
            {
                CultureInfo.GetCultureInfo("fr-FR"),
                CultureInfo.GetCultureInfo("en-US"),
            };

        }
               

        public string Translate(TranslatedKeyLabel key)
        {
            return Translate(CultureInfo.CurrentUICulture, key);
        }


        public string Translate(CultureInfo culture, TranslatedKeyLabel key)
        {
            return key.DefaultDisplay;
        }


        public CultureInfo[] AvailableCultures { get { return _availableCultures; } }


        private readonly ITranslateServiceDataAccess _dataAccess;
        private CultureInfo[] _availableCultures;


    }
}
