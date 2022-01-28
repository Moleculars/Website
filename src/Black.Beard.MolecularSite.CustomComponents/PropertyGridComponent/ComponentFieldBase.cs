using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Bb.MolecularSite.PropertyGridComponent
{

    public abstract partial class ComponentFieldBase : ComponentBase, INotifyPropertyChanged
    {

        [Parameter]
        public PropertyObjectDescriptor? Property { get; set; }


        public string? ErrorText { get; set; }


        protected void PropertyChange()
        {

            if (PropertyChanged != null && this.Property != null)
                PropertyChanged(this, new PropertyChangedEventArgs(this.Property.Parent.TranslateService.Translate(Property.Display)));

        }


        public event PropertyChangedEventHandler? PropertyChanged;


    }


}
