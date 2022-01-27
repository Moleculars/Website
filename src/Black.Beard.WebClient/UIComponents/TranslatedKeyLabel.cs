using Bb.WebClient.Exceptions;
using System.Diagnostics;

namespace Bb.WebClient.UIComponents
{

    [DebuggerDisplay("{DefaultDisplay}")]
    public class TranslatedKeyLabel
    {

        public TranslatedKeyLabel(string application, string context, string key, string defaultDisplay)
        {

            this.Application = application;
            this.Context = context;
            this.Key = key;

            this.DefaultDisplay = defaultDisplay;

        }

        public string Application { get; }

        public string Context { get; }

        public string Key { get; }

        public string DefaultDisplay { get; }

        public override string ToString()
        {
            return $"{Application}::{Context}::{Key}";
        }

        public static implicit operator TranslatedKeyLabel(string key)
        {

            if (string.IsNullOrEmpty(key))
                return new TranslatedKeyLabel(string.Empty, string.Empty, string.Empty, string.Empty);

            var items = key.Split("::");

            if (key.StartsWith("::") && items.Length == 2)
                return new TranslatedKeyLabel(string.Empty, string.Empty, items[1], items[1]);

            if (items.Length == 4)
                return new TranslatedKeyLabel(items[0], items[1], items[2], items[3]);

            if (items.Length == 3)
                return new TranslatedKeyLabel(items[0], items[1], items[2], string.Empty);

            throw new InvalidTranslationKeyFormatException(key);

        }


    }
}
