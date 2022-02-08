
using Bb.ComponentModel.Translations;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bb.CustomComponents.PropertyGridComponent
{

    public partial class PropertyGrid
    {

        [Inject]
        public ITranslateService TranslateService { get; set; }

        [Inject]
        public IServiceProvider ServiceProvider { get; set; }

        [Parameter]
        public Func<PropertyObjectDescriptor, bool> ExcludeProperties { get; set; }

        [Parameter]
        public Action<PropertyObjectDescriptor> PropertyHasChanged { get; set; }

        protected override Task OnInitializedAsync()
        {
            Descriptor = new ObjectDescriptor(null, null, TranslateService, ServiceProvider);
            return base.OnInitializedAsync();
        }

        [Parameter]
        public object SelectedObject
        {
            get => _selectedObject;
            set
            {
                if (value != null)
                {
                    _selectedObject = value;
                    var d = new ObjectDescriptor(value, value?.GetType(), TranslateService, ServiceProvider, ExcludeProperties)
                    {
                        PropertyHasChanged = this.PropertyHasChanged,
                    };
                    this.Descriptor = d;
                    this.Descriptor.PropertyHasChanged = this.SubPropertyHasChanged;
                }
                StateHasChanged();
            }
        }

        private void SubPropertyHasChanged(PropertyObjectDescriptor obj)
        {
            StateHasChanged();
            if (PropertyHasChanged != null)
                PropertyHasChanged(obj);
        }

        public ObjectDescriptor Descriptor { get; set; }


        bool success;
        string[] errors = { };
        MudForm form;
        private object _selectedObject;

    }



}
