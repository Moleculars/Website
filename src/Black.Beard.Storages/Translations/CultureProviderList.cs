using Bb.ComponentModel.DataAnnotations;
using Bb.ComponentModel.Translations;
using System.ComponentModel;
using System.Globalization;

namespace Bb.Translations
{
    public class CultureProviderList : IListProvider
    {


        public PropertyDescriptor Property { get; set; }
        

        public ITranslateService TranslateService { get; set; }


        public IEnumerable<ListItem> GetItems()
        {
            var items = CultureInfo.GetCultures(CultureTypes.FrameworkCultures);
            foreach (var item in items)
                yield return new ListItem() { Display = item.DisplayName, Name = item.EnglishName, Value = item.IetfLanguageTag };

        }

    }

}
