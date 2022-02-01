using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace MolecularSite.Shared
{

    public partial class NavMenu
    {

        [Inject]
        private UIService? uIService { get; set; }

        [Inject]
        private TranslateService? translateService { get; set; }

        public List<DynamicServerMenu>? Menus { get; set; }

        protected override async Task OnInitializedAsync()
        {

            Menus = new List<DynamicServerMenu>();
            var menuBuilder = new MenuConverter(translateService);
            if (uIService != null)
            {
                var m = await uIService.GetUI(UIService.LeftMenu);
                if (m != null)
                    foreach (var m1 in m)
                        Menus.Add((DynamicServerMenu)menuBuilder.Convert(m1));

            }

        }

    }

}
