using Bb.WebHost.UIComponents;
using Microsoft.AspNetCore.Components;

namespace MudBlazorTemplates1.Shared
{

    public partial class DynamicServerMenuComponentList
    {


        [Parameter]
        public List<DynamicServerMenu>? Menus { get; set; }



    }


}
