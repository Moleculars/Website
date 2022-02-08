using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Components;

namespace MolecularSite.Shared
{

    public partial class DynamicServerMenuComponent
    {


        [Parameter]
        public DynamicServerMenu? Menu { get; set; }


        [Parameter]
        public bool Enabled { get; set; }

    }

}
