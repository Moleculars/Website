﻿using Microsoft.AspNetCore.Components.Routing;
using System.Globalization;
using System.Linq;

namespace Bb.WebClient.UIComponents
{


    public class MenuConverter : IMenuConverter
    {

        public MenuConverter(TranslateService translateService)
        {
            this._translateService = translateService;
        }

        public object Convert(UIComponent c)
        {


            var menu = new DynamicServerMenu(c.Children.Count())
            {
                Uui = c.Uuid,
                
                Display = c.Display != null 
                    ? this._translateService.Translate(c.Display) 
                    : string.Empty,

                Type = c.Type,
                Icon = c.Icon.Value,
            };




            if (c is UIComponentMenu u)
            {
                if (u.KeyboardArrowDown)
                    menu.KeyboardArrowDown = Glyphs.GlyphFilled.KeyboardArrowDown.Value;

                if (u.Action != null)
                    menu.Action = u.Action;
                else
                    menu.Action = new ActionReference()
                    {
                        HRef = string.Empty,
                        Match = NavLinkMatch.All,
                    };
            }


            menu.Roles.AddRange(c.Roles);

            foreach (var item in c.Children)
            {
                var subMenu = (DynamicServerMenu)Convert(item);
                menu.Add(subMenu);
            }

            return menu;

        }

        private readonly TranslateService _translateService;

    }


}