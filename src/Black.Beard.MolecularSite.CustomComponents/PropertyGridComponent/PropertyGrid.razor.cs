
using Bb.ComponentModel.Translations;
using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bb.MolecularSite.PropertyGridComponent
{

    public partial class PropertyGrid
    {

        [Inject]
        public ITranslateService TranslateService { get; set; }

        [Inject]
        public IServiceProvider ServiceProvider { get; set; }

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
                    var d = new ObjectDescriptor(value, value?.GetType(), TranslateService, ServiceProvider);
                    d.Analyze();
                    this.Descriptor = d;
                }
            }
        }

        public ObjectDescriptor Descriptor { get; set; }


        bool success;
        string[] errors = { };
        MudForm form;
        private object _selectedObject;
    }



}
