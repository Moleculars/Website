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


        public string? ErrorText { get; set; }


        protected virtual void PropertyChange()
        {

            StateHasChanged();

        }

        private PropertyObjectDescriptor? _property;

    }


}
