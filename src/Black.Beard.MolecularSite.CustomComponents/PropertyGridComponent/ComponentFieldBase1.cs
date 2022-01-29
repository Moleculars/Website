using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Bb.MolecularSite.PropertyGridComponent
{

    public partial class ComponentFieldBase<T> : ComponentFieldBase
    {

        public ComponentFieldBase()
        {

        }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }


        public T? Value
        {
            get
            {

                if (Property != null)
                    return (T)Load();

                return default(T);

            }
            set
            {
                if (Property != null)
                    Property.Value = value;
            }

        }


        public T GetStep()
        {

            var step = this.Property.Step;
            var result = Convert.ChangeType(step, typeof(T));

            if (object.Equals(result, 0))
                result = 1;

            return (T)result;

        }

        public T GetMinimum()
        {

            var step = this.Property.Minimum;
            var result = Convert.ChangeType(step, typeof(T));

            if (object.Equals(result, 0))
                result = 1;

            return (T)result;

        }

        public T GetMaximum()
        {

            var step = this.Property.Maximum;
            var result = Convert.ChangeType(step, typeof(T));

            if (object.Equals(result, 0))
                result = 1;

            return (T)result;

        }

        public string Validate(T o)
        {

            if (Property != null)
            {
                var messages = new List<string>();
                if (!Property.Validate(o, messages))
                    return String.Concat(messages.Select(c => ", " + c)).Trim(',', ' ');
            }

            return null;

        }


        public object Load()
        {

            object _value = null;

            if (Property != null)
            {

                try
                {

                    var v = Property.Value;
                    var c = object.Equals(v, _value);

                    if (this.Property.KingView == PropertyKingView.Date || this.Property.KingView == PropertyKingView.DateOffset)
                    {
                        if (Property.IsNullable)
                        {
                            var o = Convert.ChangeType(v, Property.SubType, CultureInfo.CurrentCulture);
                            _value = (T)Activator.CreateInstance(this.Property.Type, new object[] { o });
                        }
                        else
                        {
                            _value = (T)Convert.ChangeType(v, this.Property.Type, CultureInfo.CurrentCulture);
                        }
                    }
                    else
                    {
                        if (Property.IsNullable)
                        {
                            var o = Convert.ChangeType(v, Property.SubType);
                            _value = (T)Activator.CreateInstance(this.Property.Type, new object[] { o });
                        }
                        else
                        {
                            _value = (T)Convert.ChangeType(v, this.Property.Type);
                        }
                    }
                    if (c)
                        PropertyChange();
                }
                catch (Exception ex)
                {

                }

            }

            return _value;

        }

    }

}
