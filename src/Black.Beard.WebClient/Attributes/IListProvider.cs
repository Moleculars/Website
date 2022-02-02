using Bb.WebClient.UIComponents;
using System.ComponentModel;

namespace Bb.Attributes
{

    public interface IListProvider
    {

        PropertyDescriptor Property { get; set; }

        TranslateService TranslateService { get; set; }

        IEnumerable<ListItem> GetItems();

    }

}
