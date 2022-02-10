using Bb.ComponentModel.Translations;
using Bb.CustomComponents;
using Bb.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;

namespace Bb.IdentityIndividual.Pages
{

    public partial class Register
    {

        public Register()
        {
            this._dictionary = new Dictionary<string, PropertyObjectDescriptor>();
        }

        [Inject]
        public ITranslateService TranslateService { get; set; }

        [Inject]
        public IServiceProvider ServiceProvider { get; set; }

        [Inject]
        public IRepository<IRegisterModel> Repository { get; set; }

        public IRegisterModel Model { get; set; }



        public ObjectDescriptor? Descriptor { get; set; }

        protected override Task OnInitializedAsync()
        {
            Model = Repository.GetNew();
            return base.OnInitializedAsync();
        }


        void OnValidate(PropertyObjectDescriptor property)
        {

            var _success = true;
            var list = new List<string>();

            foreach (var item in this._dictionary)
            {
                list.Clear();
                if (!item.Value.Validate(list))
                {
                    _success = false;
                    break;
                }
            }
            if (_success != success)
            {
                success = _success;
                StateHasChanged();
            }
        }

        void OnInitialized(PropertyObjectDescriptor property)
        {

            this._dictionary.Add(property.PropertyDescriptor.Name, property);

            success = true;
            List<string> messages = new List<string>();
            foreach (var item in this._dictionary)
                if (!item.Value.Validate(messages))
                    success = false;

            StateHasChanged();

        }

        async Task RegisterProcess()
        {
            _processing = true;
            Repository.SaveNew(this.Model);
            _processing = false;
        }

        bool _processing;
        bool success;
        private Dictionary<string, PropertyObjectDescriptor> _dictionary;

    }

}
