using System.Globalization;

namespace Bb.Translations.Services
{
    public class TranslateContainer
    {

        public TranslateContainer(string key)
        {
            this.Key = key;
            this._Containerchildren = new Dictionary<string, TranslateContainer>();
            this._children = new Dictionary<string, TranslateContainerByCulture>();
        }

        public IEnumerable<TranslateContainer> Containers { get => _Containerchildren.Values; }

        public IEnumerable<TranslateContainerByCulture> Cultures { get => _children.Values; }

        public void Sort(params TranslateServiceDataModel[] items)
        {
            
            foreach (var item in items)
            {
                Sort(item);
            }

        }

        public void Sort(IEnumerable<TranslateServiceDataModel> items)
        {
            foreach (var item in items)
            {
                Sort(item);
            }
        }

        public TranslateContainerResult Sort(TranslateServiceDataModel item)
        {
            return Sort(item, 0);
        }

        private TranslateContainerResult Sort(TranslateServiceDataModel item, int index)
        {

            if (item.Path == null || index == item.Path.Length)
            {

                if (!_children.TryGetValue(item.Key, out var children))
                    lock (_lock)
                        if (!_children.TryGetValue(item.Key, out children))
                            _children.Add(item.Key, (children = new TranslateContainerByCulture() { Key = item.Key }));

                return children.Sort(item);

            }
            else
            {

                var pathKey = item.Path[index];
                if (!_Containerchildren.TryGetValue(pathKey, out TranslateContainer container))
                    lock (_lock)
                        if (!_Containerchildren.TryGetValue(pathKey, out container))
                            _Containerchildren.Add(pathKey, (container = new TranslateContainer(pathKey)));

                return container.Sort(item, index + 1);

            }

        }

        public TranslateContainerResult Resolve(string[] path, string key, CultureInfo culture, int index, out TranslateServiceDataModel? result)
        {

            result = null;

            if (index == path.Length)
            {

                if (_children.TryGetValue(key, out var container))
                    return container.Resolve(culture, out result);

            }
            else
            {
                var pathKey = path[index];
                if (_Containerchildren.TryGetValue(pathKey, out TranslateContainer container))
                    return container.Resolve(path, key, culture, index + 1, out result);
            }

            return TranslateContainerResult.NotFound;

        }

        public string Key { get; }

        private readonly Dictionary<string, TranslateContainer> _Containerchildren;
        private readonly Dictionary<string, TranslateContainerByCulture> _children;
        private volatile object _lock = new object();

    }

}
