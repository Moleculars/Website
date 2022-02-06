using Bb.Translations.Services;
using Bb.WebClient.UIComponents;

namespace Bb.Translations.Pages
{

    public class TranslationTreeItemData
    {

        public TranslationTreeItemData(object container)
        {

            this.Tag = container;

            if (container != null)
            {
                TreeItems = new HashSet<TranslationTreeItemData>();

                var create = false;
                if (this.Tag is TranslateContainer c)
                    create = c.Containers.Any() || c.Cultures.Any();


                if (this.Tag is TranslateContainerByCulture cc)
                    create = cc.Items.Any();

                if (create)
                    this.TreeItems.Add(new TranslationTreeItemData(null));
            }

        }

        public string Title { get; set; }

        public string Icon { get; set; }

        public int? Number { get; set; }

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                if (!isBuilt & isExpanded)
                    lock (_lock)
                        Build();
            }
        }

        private void Build()
        {


            isBuilt = true;
            this.TreeItems.Clear();


            if (this.Tag is TranslateContainer c)
            {

                foreach (var item in c.Containers)
                    this.TreeItems.Add(new TranslationTreeItemData(item)
                    {
                        Title = item.Key,
                        Icon = WebClient.UIComponents.Glyphs.GlyphFilled.AccountTree.Value,
                    });

                foreach (var item in c.Cultures)
                    this.TreeItems.Add(new TranslationTreeItemData(item)
                    {
                        Title = item.Key,
                        Icon = WebClient.UIComponents.Glyphs.GlyphFilled.Translate.Value,
                    });

            }

        }

        public HashSet<TranslationTreeItemData> TreeItems { get; }

        public object Tag { get; }

        public override bool Equals(object x)
        {
            var other = x as TranslationTreeItemData;
            if (other == null)
                return false;
            return other.Title == Title;
        }

        public override int GetHashCode()
        {
            return Title?.GetHashCode() ?? 0;
        }

        private volatile object _lock = new object();
        private readonly Func<TranslationTreeItemData, TranslateServiceDataModel, bool> _function;
        private bool isExpanded;
        private bool isBuilt;

    }




}
