using System.Globalization;

namespace Bb.Translations.Services
{
    public class TranslateContainerByCulture
    {

        public TranslateContainerByCulture()
        {
            _children = new Dictionary<CultureInfo, TranslateServiceDataModel>();
        }

        public IEnumerable<TranslateServiceDataModel> Items { get => _children.Values; }


        public string Key { get; internal set; }

        public TranslateContainerResult Sort(TranslateServiceDataModel item)
        {

            var culture = item.Culture;

            if (!_children.TryGetValue(culture, out TranslateServiceDataModel model))
                lock (_lock)
                    if (!_children.TryGetValue(culture, out model))
                    {
                        _children.Add(culture, item);
                        return TranslateContainerResult.Added;
                    }


            if (model._id == item._id)
                lock (_lock)
                {
                    _children[culture] = item;
                    return  TranslateContainerResult.Aligned;
                }
            

            return TranslateContainerResult.DuplicatedKey;


        }

        public TranslateContainerResult Resolve(CultureInfo culture, out TranslateServiceDataModel result)
        {

            if (_children.TryGetValue(culture, out result))
                return TranslateContainerResult.Resolved;

            return TranslateContainerResult.NotFound;

        }

        private readonly Dictionary<CultureInfo, TranslateServiceDataModel> _children;
        private volatile object _lock = new object();

    }

}
