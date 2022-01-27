using Bb.WebHost.UIComponents;
using Microsoft.AspNetCore.Components;

namespace MolecularSite.Shared
{

    public partial class DynamicServerMenuComponentList
    {


        [Parameter]
        public List<DynamicServerMenu>? Menus { get; set; }



    }


}
