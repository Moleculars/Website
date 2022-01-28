
using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bb.MolecularSite.PropertyGridComponent
{

    public partial class PropertyGrid
    {

        [Inject]
        public TranslateService TranslateService { get; set; }


        protected override Task OnInitializedAsync()
        {
            Descriptor = new ObjectDescriptor(null, TranslateService);
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
                    var d = new ObjectDescriptor(value, TranslateService);
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
