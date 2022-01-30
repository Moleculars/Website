using Bb.ComponentModel.Attributes;
using Bb.WebClient.Exceptions;
using System.Diagnostics;
using System.Reflection;

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

        public static TranslatedKeyLabel EmptyKey { get; } = new TranslatedKeyLabel(string.Empty, string.Empty, string.Empty, string.Empty);


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
                return TranslatedKeyLabel.EmptyKey;

            if (!key.Contains("::"))
                key = "::" + key;

            var items = key.Split("::");

            if (key.StartsWith("::") && items.Length == 2)
                return new TranslatedKeyLabel(string.Empty, string.Empty, items[1], items[1]);

            if (items.Length == 4)
                return new TranslatedKeyLabel(items[0], items[1], items[2], items[3]);

            if (items.Length == 3)
            {

                return new TranslatedKeyLabel(items[0], items[1], items[2], string.Empty);

            }

            throw new InvalidTranslationKeyFormatException(key);

        }

        public static bool IsValidKey(string p)
        {
            return p.Contains("::");
        }

        public override bool Equals(object? obj)
        {

            if (obj is TranslatedKeyLabel key)
                return key.ToString() == this.ToString();

            return false;

        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }


        public static IEnumerable<TranslatedKeyLabel> GetFrom(MemberInfo info)
        {

            var items = info.GetCustomAttributes<TranslationKeyAttribute>()
                .ToList();

            foreach (TranslationKeyAttribute attribute in items)
                yield return (TranslatedKeyLabel)attribute.Key;

        }


    }

}

/*
 
    ,,,d:Default value
 
    a:application, c:context, k:key, d:default value


 */