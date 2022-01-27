using System.Diagnostics;

namespace MudBlazorTemplates1.Shared
{

    [DebuggerDisplay("{Display}")]
    public class DynamicServerMenu 
        : List<DynamicServerMenu>
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

    }
}
