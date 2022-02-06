﻿using Bb.ComponentModel;
using Bb.ComponentModel.Attributes;
using Bb.ComponentModel.Translations;
using System.Globalization;

namespace Bb.Translations.Services
{


    [ExposeClass(ConstantsCore.Initialization, ExposedType = typeof(ITranslateService), LifeCycle = IocScopeEnum.Singleton)]
    public class TranslateService : ITranslateService
    {


        public TranslateService(TranslateServiceDataAccess dataAccess)
        {

            DataAccess = dataAccess;

        }

        public TranslateServiceDataAccess DataAccess { get; }


        public string Translate(TranslatedKeyLabel key)
        {
            return Translate(CultureInfo.CurrentUICulture, key);
        }

        public object Container { get => datas; }

        public CultureInfo[] AvailableCultures => DataAccess.AvailableCultures;

        public string Translate(CultureInfo culture, TranslatedKeyLabel label)
        {

            if (!label.IsNotValidKey)
            {

                if (datas == null)
                    lock (_lock)
                        if (datas == null)
                        {
                            datas = new TranslateContainer(string.Empty);
                            var d = DataAccess.GetAll().ToList();
                            datas.Sort(d);
                        }

                TranslateContainerResult r;

                if ((r = datas.Resolve(DataAccess.SplitPath(label.Path), label.Key, culture, 0, out TranslateServiceDataModel result)) != TranslateContainerResult.Resolved)
                    lock (_lock)
                        if ((r = datas.Resolve(DataAccess.SplitPath(label.Path), label.Key, culture, 0, out result)) != TranslateContainerResult.Resolved)
                            foreach (var item in label.Datas)
                            {
                                var result1 = DataAccess.GetNew(label.Path, label.Key);
                                result1.Culture = item.Value.Culture;
                                result1.Value = item.Value.Value ?? label.DefaultDisplay;
                                var sorted = datas.Sort(result1);
                                if (result1.Culture.IetfLanguageTag == culture.IetfLanguageTag)
                                    result = result1;
                            }


                foreach (var cultureItem in this.AvailableCultures)
                    if ((r = datas.Resolve(DataAccess.SplitPath(label.Path), label.Key, cultureItem, 0, out result)) != TranslateContainerResult.Resolved)
                        lock (_lock)
                            if ((r = datas.Resolve(DataAccess.SplitPath(label.Path), label.Key, cultureItem, 0, out result)) != TranslateContainerResult.Resolved)
                                foreach (var item in label.Datas)
                                {
                                    var result1 = DataAccess.GetNew(label.Path, label.Key);
                                    result1.Culture = cultureItem;
                                    result1.Value = item.Value.Value ?? label.DefaultDisplay;
                                    var sorted = datas.Sort(result1);
                                    if (result1.Culture.IetfLanguageTag == culture.IetfLanguageTag)
                                        result = result1;
                                }


                if (result != null)
                    return result.Value;

                else
                {

                }

            }

            return label.DefaultDisplay;

        }

        private TranslateContainer datas;
        private volatile object _lock = new object();

    }

}
