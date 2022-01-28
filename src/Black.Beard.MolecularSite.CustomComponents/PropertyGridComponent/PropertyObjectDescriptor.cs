using Bb.Attributes;
using Bb.WebClient.UIComponents;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Bb.MolecularSite.PropertyGridComponent
{

    public class PropertyObjectDescriptor
    {

        public PropertyObjectDescriptor(PropertyDescriptor property, ObjectDescriptor parent)
        {

            this.Parent = parent;
            this._property = property;

            this.Display = property.DisplayName ?? property.Name;
            this.Description = property.Description;
            this.Category = property.Category;

            this.Browsable = true;
            this.ReadOnly = false;
            this.DefaultValue = null;
            this.Minimum = Int32.MinValue;
            this.Maximum = Int32.MaxValue;
            this.Step = 1;
            this._constraints = new List<ValidationAttribute>();
            this.Type = _property.PropertyType;



            if (this.Type.IsGenericType && this.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                this.IsNullable = true;
                this.SubType = Type.GetGenericArguments()[0];
            }
            else
                this.SubType = typeof(void);


            if (this.Type == typeof(char))
                this.KingView = PropertyKingView.Char;

            else if (this.Type == typeof(string))
                this.KingView = PropertyKingView.String;

            else if (this.Type == typeof(bool) || this.Type == typeof(bool?))
                this.KingView = PropertyKingView.Bool;

            else if (this.Type == typeof(Int16) || this.Type == typeof(Int16?))
                this.KingView = PropertyKingView.Int16;

            else if (this.Type == typeof(Int32) || this.Type == typeof(Int32?))
                this.KingView = PropertyKingView.Int32;

            else if (this.Type == typeof(Int64) || this.Type == typeof(Int64?))
                this.KingView = PropertyKingView.Int64;

            else if (this.Type == typeof(UInt16) || this.Type == typeof(UInt16?))
                this.KingView = PropertyKingView.UInt16;

            else if (this.Type == typeof(UInt16) || this.Type == typeof(UInt16?))
                this.KingView = PropertyKingView.UInt32;

            else if (this.Type == typeof(UInt64) || this.Type == typeof(UInt64?))
                this.KingView = PropertyKingView.UInt64;

            else if (this.Type == typeof(DateTime) || this.Type == typeof(DateTime?))
                this.KingView = PropertyKingView.Date;

            else if (this.Type == typeof(DateTimeOffset) || this.Type == typeof(DateTimeOffset?))
                this.KingView = PropertyKingView.DateOffset;

            else if (this.Type == typeof(TimeSpan) || this.Type == typeof(TimeSpan?))
                this.KingView = PropertyKingView.Time;

            else if (this.Type == typeof(float) || this.Type == typeof(float?))
                this.KingView = PropertyKingView.Float;

            else if (this.Type == typeof(Double) || this.Type == typeof(Double?))
                this.KingView = PropertyKingView.Double;

            else if (this.Type == typeof(Decimal) || this.Type == typeof(Decimal?))
                this.KingView = PropertyKingView.Decimal;

            else if (typeof(IEnumerable).IsAssignableFrom(this.Type))
            {

                foreach (var item in this.Type.GetInterfaces())
                {
                    if (item.IsGenericType && item.GetGenericTypeDefinition() is Type type && type == typeof(ICollection<>))
                    {
                        this.SubType = this.Type.GetGenericArguments()[0];
                        this.KingView = PropertyKingView.List;
                    }
                }


            }


        }


        public object? Value
        {
            get
            {

                var result = _property.GetValue(Parent.Instance);

                if (result == null)
                    return this.DefaultValue;

                return result;

            }

            set
            {
                _property.SetValue(Parent.Instance, value);
            }

        }


        internal void Analyze()
        {

            foreach (Attribute attribute in _property.Attributes)
            {
                if (attribute.GetType().Namespace != "System.Runtime.CompilerServices")

                    switch (attribute)
                    {

                        case CategoryAttribute:
                        case DisplayNameAttribute:
                        case DescriptionAttribute:
                            break;

                        case System.ComponentModel.PasswordPropertyTextAttribute passwordPropertyText:
                            IsPassword = passwordPropertyText.Password;
                            break;

                        case StepNumericAttribute stepNumeric:
                            this.Step = stepNumeric.Step;
                            break;

                        case StringLengthAttribute stringLength:
                            this.Maximum = stringLength.MaximumLength;
                            this.Minimum = stringLength.MinimumLength;
                            break;

                        case MaxLengthAttribute maxLength:
                            this.Maximum = maxLength.Length;
                            break;

                        case MinLengthAttribute minLength:
                            this.Maximum = minLength.Length;
                            break;

                        case RangeAttribute range:
                            this.Minimum = (int)range.Minimum;
                            this.Maximum = (int)range.Maximum;
                            break;

                        case DisplayFormatAttribute displayFormat:
                            this.DataFormatString = displayFormat.DataFormatString;
                            this.HtmlEncode = displayFormat.HtmlEncode;
                            break;

                        case EditableAttribute editable:
                            this.Browsable = editable.AllowEdit;
                            break;

                        case BrowsableAttribute browsable:
                            this.Browsable = browsable.Browsable;
                            break;

                        case ReadOnlyAttribute readOnly:
                            this.ReadOnly = readOnly.IsReadOnly;
                            break;

                        case DefaultValueAttribute defaultValue:
                            this.DefaultValue = defaultValue.Value;
                            break;

                        case EditorAttribute editor:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                        case PropertyTabAttribute propertyTab:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                        case TypeConverterAttribute typeConverter:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                        case TypeDescriptionProviderAttribute typeDescriptionProvider:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                        case Bb.ComponentModel.Attributes.TranslationKeyAttribute translationKey:
                            break;

                        default:
                            break;

                    }


                if (attribute is ValidationAttribute validation)
                {
                    this._constraints.Add(validation);
                    if (validation is RequiredAttribute required)
                        this.Required = true;
                }

            }

        }


        public bool Validate(object value, List<string> messages)
        {

            foreach (var item in _constraints)
                if (!item.IsValid(value))
                {

                    var label = this.Parent.TranslateService.Translate(this.Display);
                    var message = item.FormatErrorMessage(label);

                    if (TranslatedKeyLabel.IsValidKey(item.FormatErrorMessage(string.Empty)))
                        messages.Add(this.Parent.TranslateService.Translate(message));
                    else
                        messages.Add(message);

                }

            return messages.Count == 0;

        }


        public Type Type { get; }


        public string GetDisplay()
        {
            return Parent.TranslateService.Translate(this.Display);
        }

        public string GetDescription()
        {
            return Parent.TranslateService.Translate(this.Description);
        }

        public string GetCategory()
        {
            return Parent.TranslateService.Translate(this.Category);
        }

        public TranslatedKeyLabel Display { get; private set; }

        public TranslatedKeyLabel Description { get; private set; }

        public TranslatedKeyLabel Category { get; private set; }

        public bool Browsable { get; private set; }

        public bool ReadOnly { get; private set; }

        public object? DefaultValue { get; private set; }

        public bool Required { get; private set; }

        public string? DataFormatString { get; private set; }

        public bool HtmlEncode { get; private set; }

        public ObjectDescriptor Parent { get; }

        public PropertyKingView KingView { get; private set; }

        public bool IsNullable { get; }
        public Type SubType { get; }

        public int Minimum { get; private set; }

        public int Maximum { get; private set; }

        public float Step { get; private set; }
        public bool IsPassword { get; private set; }

        private readonly PropertyDescriptor _property;
        private readonly List<ValidationAttribute> _constraints;

    }

    //public class TypeSwitch
    //{
    //    public TypeSwitch Case<T>(Action<T> action)
    //    {
    //        matches.Add(typeof(T), (x) => action((T)x));
    //        return this;
    //    }
    //    public void Switch(object x) { matches[x.GetType()](x); }
    //    private Dictionary<Type, Action<object>> matches = new Dictionary<Type, Action<object>>();
    //}


    //public class TypeSwitch<T1>
    //{
    //    public TypeSwitch<T1> Case<T>(Func<T, T1> action)
    //    {
    //        matches.Add(typeof(T), (x) => action((T)x));
    //        return this;
    //    }
    //    public void Switch(object x) { matches[x.GetType()](x); }
    //    private Dictionary<Type, Func<object, T1>> matches = new Dictionary<Type, Func<object, T1>>();
    //}

}
