using Bb.WebHost.UIComponents;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace MudBlazorTemplates1.Shared
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
                       
            //Menus = new List<DynamicServerMenu>();
            //var menuBuilder = new MenuConverter(CultureInfo.CurrentCulture, translateService);
            //if (uIService != null)
            //{
            //    var m = await uIService.GetUI(UIService.LeftMenu);
            //    foreach (var m1 in m)
            //        Menus.Add((DynamicServerMenu)menuBuilder.Convert(m1));

            //}




        }

    }

}
