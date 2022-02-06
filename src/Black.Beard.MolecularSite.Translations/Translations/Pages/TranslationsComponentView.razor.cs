using Bb.ComponentModel.Translations;
using Bb.MolecularSite;
using Bb.Translations.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Globalization;

namespace Bb.Translations.Pages
{

    public partial class TranslationsComponentView
    {

        [Inject]
        public TranslateServiceDataAccess DataService { get; set; }

        
        [Inject]
        public ITranslateService TranslateService { get; set; }


        [Inject]
        public TranslateServiceByRemote TranslateServiceByRemote { get; set; }

        [Inject]
        public IDialogService DialogService { get;set; }

        public CultureInfo Culture { get; set; }


        protected override Task OnAfterRenderAsync(bool firstRender)
        {
          
            var serv = TranslateService as TranslateService;
            if (serv != null)
                TreeItems.Add(new TranslationTreeItemData(serv.Container)
                {
                    Title = "Root",

                });

            StateHasChanged();

            return base.OnAfterRenderAsync(firstRender);

        }

        private TranslationTreeItemData ActivatedValue { get; set; }

        private IEnumerable<TranslateServiceDataModel> Values
        {
            get
            {
                if (ActivatedValue != null)
                {
                    var o = ActivatedValue.Tag as TranslateContainerByCulture;
                    if (o != null)
                        foreach (TranslateServiceDataModel item in o.Items)
                            yield return item;
                }
            }
        }


        public  void Translate()
        {

            var cultureSource = this.Culture;
            var itemSource = Values.First(c => c.Culture.IetfLanguageTag == cultureSource.IetfLanguageTag);
            
            var target = _item.Culture;

            _item.Value = this.TranslateServiceByRemote.Translate(cultureSource, target, itemSource.Value);
            
        }

        public async void Show(TranslateServiceDataModel item)
        {         
            _item = item;

            bool? result = await mbox.Show();
            // state = result == null ? "Cancelled" : "Deleted!";
            StateHasChanged();

        }

        public void Save()
        {

            DialogOptions options = new DialogOptions() { CloseButton = false, CloseOnEscapeKey = true };

            IDialogReference reference = DialogService.Show<DialogWaitingProssessDone>("     Saving translations     ", options);
            StateHasChanged();

            DataService.Save(TranslateService, () => reference.Result.IsCompleted);

            reference.Close();



        }

        public void Cancel()
        {


        }

        private HashSet<TranslationTreeItemData> SelectedValues;
        private HashSet<TranslationTreeItemData> TreeItems = new HashSet<TranslationTreeItemData>();
        private TranslateServiceDataModel _item;
        private MudMessageBox mbox { get; set; }


    }


}
