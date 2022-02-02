using Bb.ComponentModel.Translations;
using Bb.MolecularSite.PropertyGridComponent;
using Bb.WebClient.UIComponents;
using Microsoft.AspNetCore.Components;

namespace Bb.MolecularSite.DataGrid
{
    public partial class ComponentGrid<T>
    {


        [Inject]
        public ITranslateService TranslateService { get; set; }

        public ObjectDescriptor Descriptor { get; set; }


        [Inject]
        public IServiceProvider ServiceProvider { get; set; }

        [Parameter]
        public List<T> Datas { get; set; }

        protected override Task OnInitializedAsync()
        {
            this.Descriptor = new ObjectDescriptor(null, typeof(TranslatedKeyLabel), TranslateService, ServiceProvider);
            this.Descriptor.Analyze();

            var result = base.OnInitializedAsync();
            return result;
        }

        public IEnumerable<PropertyObjectDescriptor> Headings()
        {
            if (this.Descriptor != null && this.Descriptor?.Items != null)
                foreach (PropertyObjectDescriptor item in this.Descriptor.Items)
                    yield return item;
        }

        public IEnumerable<T> Rows
        {
            get
            {
                if (this.Descriptor != null)
                    if (Datas != null)
                        foreach (T item in Datas)
                        {
                            this.Descriptor.Instance = item;
                            yield return item;
                        }

            }
        }

    }

}
