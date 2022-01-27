﻿namespace Bb.WebHost.Exceptions
{


    [System.Serializable]
    public class InvalidTranslationKeyFormatException : System.Exception
    {

        public InvalidTranslationKeyFormatException() { }

        public InvalidTranslationKeyFormatException(string key) 
            : base($"Invalid translation key format '{key}'. The format is {{application key}}::{{context key}}::{{key of the translation}}::{{default value if translation is missing}}") 
        {
            this.Key = key;
        }

        public InvalidTranslationKeyFormatException(string key, System.Exception inner) 
            : base($"Invalid translation key format '{key}'. The format is {{application key}}::{{context key}}::{{key of the translation}}::{{default value if translation is missing}}", inner) 
        {
            this.Key = key;
        }

        protected InvalidTranslationKeyFormatException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public string Key { get; set; }

    }

}