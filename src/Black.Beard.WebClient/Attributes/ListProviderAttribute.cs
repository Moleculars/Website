using Bb.WebClient.UIComponents;
using System.ComponentModel;

namespace Bb.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    public sealed class ListProviderAttribute : Attribute
    {

        // This is a positional argument
        public ListProviderAttribute(Type typeListResolver)
        {
            this.EnumerationResolver = typeListResolver;
        }

        public Type EnumerationResolver { get; }

    }


    public interface IListProvider
    {


        PropertyDescriptor Property { get; set; }

        TranslateService TranslateService { get; set; }


        IEnumerable<ListItem> GetItems();


    }

    public class ListItem
    {

        public string Name { get; set; }

        public string Display { get; set; }

        public object Value { get; set; }

        public override string ToString()
        {
            return Display.ToString();
        }

        public override bool Equals(object o)
        {
            var other = o as ListItem;
            return other?.Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

    }

}
