using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Bb.CustomComponents.PropertyGridComponent
{

    public partial class ComponentFieldBase : ComponentBase
    {

        [Parameter]
        public PropertyObjectDescriptor? Property 
        {
            get => _property;
            set
            {
                _property = value;
                PropertyChange();                
            }
        }

        public bool Changed { get; internal set; }


        protected virtual void PropertyChange()
        {
            this.Changed = true;
            if (_property != null)
            {
                
                if (_property.PropertyHasChanged != null)
                    _property.PropertyHasChanged(_property);

                StateHasChanged();

            }

        }

        private PropertyObjectDescriptor? _property;

    }


}
