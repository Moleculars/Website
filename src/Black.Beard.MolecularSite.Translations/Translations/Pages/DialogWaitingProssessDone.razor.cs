using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bb.Translations.Pages
{
    public partial class DialogWaitingProssessDone
    {

        [CascadingParameter] 
        public MudDialogInstance MudDialog { get; set; }

        public bool Cancel { get; private set; }

        public void Close()
        { 
        
            MudDialog.Close(DialogResult.Ok(false));
            this.Cancel = true;
        }
            

    }
}
