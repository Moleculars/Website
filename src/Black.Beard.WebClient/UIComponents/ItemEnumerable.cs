
namespace Bb.WebClient.UIComponents
{

    public class ItemEnumerable
    {

        public ItemEnumerable()
        {
            Label = string.Empty;
            Subs = new List<ItemEnumerable>();
            Keys = new Dictionary<string, string>();
        }

        public TranslatedKeyLabel Label { get; protected set; }


        public List<ItemEnumerable> Subs { get; set; }

        public Dictionary<string, string> Keys { get; protected set; }


    }



}


