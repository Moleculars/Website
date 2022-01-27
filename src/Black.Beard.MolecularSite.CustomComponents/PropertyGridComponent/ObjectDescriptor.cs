using System.ComponentModel;

namespace Bb.MolecularSite.PropertyGridComponent
{

    public class ObjectDescriptor
    {

        public ObjectDescriptor(object instance)
        {
            if (instance != null)
            {
                this._instance = instance;
                this._type = instance.GetType();
                this._items = new List<PropertyObjectDescriptor>();
            }
        }

        public void Analyze()
        {

            var properties = TypeDescriptor.GetProperties(this._type);
            foreach (PropertyDescriptor property in properties)
            {

                var p = new PropertyObjectDescriptor(property);
                _items.Add(p);
                p.Analyze();
            }

        }

        public IEnumerable<PropertyObjectDescriptor> Items { get => _items; }

        private readonly object _instance;
        private readonly Type _type;
        private readonly List<PropertyObjectDescriptor> _items;

    }


}
