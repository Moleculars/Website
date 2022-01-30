using Bb.WebClient.UIComponents;
using System.ComponentModel;

namespace Bb.MolecularSite.PropertyGridComponent
{

    public class ObjectDescriptor
    {

        public ObjectDescriptor(object? instance, TranslateService translateService)
        {
            if (instance != null)
            {
                this.TranslateService = translateService;
                this.Instance = instance;
                this._type = instance.GetType();
                this._items = new List<PropertyObjectDescriptor>();
            }
        }

        public void Analyze()
        {

            var properties = TypeDescriptor.GetProperties(this._type);
            foreach (PropertyDescriptor property in properties)
            {

                var p = new PropertyObjectDescriptor(property, this);
                _items.Add(p);
                p.AnalyzeAttributes();
            }

        }

        public TranslateService TranslateService { get; }

        public object Instance { get; set; }


        public IEnumerable<TranslatedKeyLabel> Categories()
        {

            var result = _items
                .Where(c => c.Browsable)
                .Select(x => x.Category).ToList();

            var h = new HashSet<string>();
            foreach (var item in result)
                if (h.Add(item.ToString()))
                    yield return item;

        }

        public IEnumerable<PropertyObjectDescriptor> ItemsByCategories(TranslatedKeyLabel category)
        {

            var c = category.ToString();

            var result = _items
                .Where(c => c.Browsable)
                .Where(x => x.Category.ToString() == c)
                // .OrderBy(c => c.Display.ToString())
                ;

            foreach (var item in result)
                yield return item;

        }

        public IEnumerable<PropertyObjectDescriptor> Items { get => _items; }

        private readonly Type _type;
        private readonly List<PropertyObjectDescriptor> _items;

    }


}
