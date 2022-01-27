
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bb.MolecularSite.PropertyGridComponent
{

    public partial class PropertyGrid
    {


        protected override Task OnInitializedAsync()
        {
            Descriptor = new ObjectDescriptor(null);
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
                    var d = new ObjectDescriptor(value);
                    d.Analyze();
                    this.Descriptor = d;
                }
            }
        }

        public ObjectDescriptor Descriptor { get; set; }


        bool success;
        string[] errors = { };
        MudTextField<string> pwField1;
        MudForm form;
        private object _selectedObject;
    }



}
