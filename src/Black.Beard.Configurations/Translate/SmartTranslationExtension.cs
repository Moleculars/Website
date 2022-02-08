using Bb.ComponentModel.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Translate
{

    public static class SmartTranslationExtension
    {

        public static TranslatedKeyLabel GetLabel(this DescriptionAttribute self)
        {

            if (self.Description.IsValidTranslationKey(out TranslatedKeyLabel key))
                return key;

            string p = string.Empty;
            var stack = new StackTrace();
            var frame = stack.GetFrame(1);
            if (frame != null)
            {
                var m = frame.GetMethod();
                if (m != null)
                    p = m.DeclaringType.Name;
            }

            return new TranslatedKeyLabel(p, nameof(DescriptionAttribute), self.Description, CultureInfo.GetCultureInfo("en-us"));

        }

    }

}
