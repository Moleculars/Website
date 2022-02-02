using Bb.WebClient.Startings;
using Bb.WebClient.UIComponents;
using System.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel;
using System.Reflection;
using Bb.Storages.ConfigurationProviders.SqlServer;
using Bb.ComponentModel.DataAnnotations;
using Bb.ComponentModel.Translations;

namespace Bb.Configurations.services
{

    public class ServiceConfigurationRepository
    {

        public ServiceConfigurationRepository(InitializationLoader loader, SqlServerConfigurationDataAccess datas, IServiceProvider provider)
        {
            this._loader = loader;
            this._datas = datas;
            this._provider = provider;
        }

        public object Get(Type type)
        {
            return this._provider.GetService(type);
        }

        public void Save(object instance, string sectionName)
        {

            var setting = this._datas.LoadConfiguration(sectionName);
            if (setting != null)
            {
                setting.Update(instance);
                if (setting.IsDirty)
                    this._datas.UpdateConfiguration(setting);
            }
            else
            {
                setting = this._datas.GetNew(sectionName, "", "json");
                setting.Update(instance);
                this._datas.InsertConfiguration(setting);
            }

        }


        public ItemEnumerable[] GetConfigurationsTypes()
        {

            Dictionary<string, ItemEnumerable<Assembly>> _dic = new Dictionary<string, ItemEnumerable<Assembly>>();

            foreach (Type item in _loader.Configurations.ToArray())
            {

                if (!_dic.TryGetValue(item.Assembly.FullName, out ItemEnumerable<Assembly>? list))
                {
                    var n = item.Assembly.GetName().Name;
                    var labelAssembly = new TranslatedKeyLabel(nameof(ServiceConfigurationRepository), n, n);
                    _dic.Add(item.Assembly.FullName, list = new ItemEnumerable<Assembly>(item.Assembly, labelAssembly));
                }

                var attribute = item
                    .GetAttributes<TranslationKeyAttribute>(c => c.Context == WebClientConstants.MenuList)
                    .FirstOrDefault()
                    ?.Key
                    ;

                TranslatedKeyLabel label = attribute ?? new TranslatedKeyLabel(item.Name, WebClientConstants.MenuList, item.Name);
                list.Subs.Add(new ItemEnumerable<Type>(item, label));

            }

            //HashSet<Assembly> _h = new HashSet<Assembly>();
            //var items = TypeDiscovery.Instance
            //    .GetTypes((t) =>
            //    {
            //        _h.Add(t.Assembly);
            //        if (typeof(System.Configuration.ConfigurationElementCollection).IsAssignableFrom(t))
            //            return true;
            //        if (t.BaseType == typeof(System.Configuration.ConfigurationSection))
            //            return true;
            //            //if (t.Assembly == typeof(System.Configuration.ConnectionStringSettingsCollection).Assembly)
            //            //return true;
            //        return false;
            //    })
            //    .ToList();
            //var _items = _h.OrderBy(c => c.FullName).ToList();

            return _dic.Values.ToArray();

        }

        private readonly InitializationLoader _loader;
        private readonly SqlServerConfigurationDataAccess _datas;
        private readonly IServiceProvider _provider;

    }


}
