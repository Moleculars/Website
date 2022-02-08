using Microsoft.AspNetCore.Components;

namespace Bb.CustomComponents.PropertyGridComponent
{

    public partial class ComponentTime
    {


        public ComponentTime()
        {

        }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        public TimeSpan? Time
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value.Value;
            }
        }


    }

}
