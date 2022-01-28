using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Components;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace Bb.MolecularSite.PropertyGridComponent
{

    public partial class ComponentFieldBaseList : ComponentFieldBase, INotifyCollectionChanged
    {


        public void Apply()
        {
            if (Property != null)
            {
                Property.Value = _value;
            }
        }


        public void Reset()
        {

            if (Property != null)
            {

                try
                {

                    _value = Property.Value;

                    this.Descriptor = new ObjectDescriptor(_value, this.Property.Parent.TranslateService);

                }
                catch (Exception ex)
                {

                }

            }

        }

        public IEnumerable<PropertyObjectDescriptor> Headings()
        {

            if (this.Descriptor == null)
                Reset();

            foreach (PropertyObjectDescriptor item in this.Descriptor.Items)
                yield return item;

        }

        public IEnumerable<object> Rows
        {
            get
            {
                if (this.Descriptor == null)
                    Reset();

                var items = Property.Value as IEnumerable;

                foreach (object item in items)
                    yield return item;
            }
        }


        public void Add()
        {
            var newItem = Activator.CreateInstance(Property.SubType);
            var method = this.Property.Type.GetMethod("Add");
            method.Invoke(_value, new object[] { newItem });

            if (this.CollectionChanged != null)
                this.CollectionChanged(this,new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItem));

            PropertyChange();

        }

        public void Del()
        {



        }

        public void Edit(object item)
        {
            CurrentItem = item; 
            isVisible = true;
            StateHasChanged();

        }

        public object CurrentItem { get; set; }

        public ObjectDescriptor Descriptor { get; private set; }

        private object _value;
        protected bool isVisible;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
    }


}
