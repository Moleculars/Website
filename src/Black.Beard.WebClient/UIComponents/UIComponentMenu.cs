using Microsoft.AspNetCore.Components.Routing;
using System;

namespace Bb.WebClient.UIComponents
{
    public class UIComponentMenu : UIComponent
    {

        public UIComponentMenu(Guid? uuid, TranslatedKeyLabel? display = null)
            : base(uuid, display)
        {

            KeyboardArrowDown = true;

        }

        public bool KeyboardArrowDown { get; private set; }

        public ActionReference? Action { get; private set; }

        public UIComponent SetKeyboardArrowDown(bool activated)
        {
            KeyboardArrowDown = activated;
            return this;
        }

        public UIComponentMenu SetAction(NavLinkMatch match, string href)
        {
            
            this.Action = new ActionReference()
            {
                Match = match,
                HRef = href,
            };

            return this;

        }

        public UIComponentMenu SetActionMatchAll()
        {

            this.Action = new ActionReference()
            {
                Match = NavLinkMatch.All,
                HRef = string.Empty,
            };

            return this;


        }
    }



}


