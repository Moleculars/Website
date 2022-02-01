//using Bb.Attributes;
//using Bb.ComponentModel;
//using Bb.WebClient.UIComponents;
//using System.ComponentModel;

//namespace Bb.Configurations
//{

//    public class TypeListProvider : IListProvider
//    {

//        public PropertyDescriptor Property { get; set; }

//        public TranslateService TranslateService { get; set; }

//        public IEnumerable<ListItem> GetItems()
//        {

//            foreach (var item in TypeDiscovery.Instance.GetTypes((t) => true))
//                yield return new ListItem() 
//                { 
//                    Value = item, 
//                    Name = item.FullName ?? item.Name, 
//                    Display = item.Name 
//                };

//        }

//    }

//}
