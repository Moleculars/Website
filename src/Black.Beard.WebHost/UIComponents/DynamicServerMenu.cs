using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bb.WebHost.UIComponents
{

    [DebuggerDisplay("{Display}")]
    public class DynamicServerMenu : List<DynamicServerMenu>
    {

        public DynamicServerMenu()
        {
            this.Roles = new List<string>();

        }

        public DynamicServerMenu(int capacity)
            : base(capacity)
        {
            this.Roles = new List<string>();

        }

        public string? Display { get; set; }

        public Guid Uui { get; set; }

        public string? Type { get; set; }

        public List<string> Roles { get; set; }

        public string Icon { get; set; }

        public string KeyboardArrowDown { get; set; }

        public ActionReference Action { get; set; }

    }


}
