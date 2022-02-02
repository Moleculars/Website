using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.Translations;
using Bb.Configurations;
using Bb.MolecularSite.PropertyGridComponent;
using Bb.WebClient.UIComponents;
using BlazorPropertyGridComponents.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.ComponentModel;

namespace Bb.Translations.Pages
{

    public partial class TranslationsComponentView
    {

        public List<TranslatedKeyLabel> Translations { get; set; } = new List<TranslatedKeyLabel>();

    }

}
