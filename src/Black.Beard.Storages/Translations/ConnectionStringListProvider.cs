using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.DataAnnotations;
using Bb.ComponentModel.Translations;
using Bb.Sql;
using System.ComponentModel;

namespace Bb.Translations
{
    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(ConnectionStringListProvider), LifeCycle = IocScopeEnum.Transiant)]
    public class ConnectionStringListProvider : IListProvider
    {

        public ConnectionStringListProvider(ConnectionSettings settings)
        {
            this._settings = settings;
        }

        public PropertyDescriptor Property { get ; set; }
        public ITranslateService TranslateService { get; set ; }

        public IEnumerable<ListItem> GetItems()
        {

            foreach (var item in _settings.ConnectionStringSettings)
                yield return new ListItem() { Value = item.Name, Name = item.Name, Display = item.Name };

        }

        private readonly ConnectionSettings _settings;


    }
}
