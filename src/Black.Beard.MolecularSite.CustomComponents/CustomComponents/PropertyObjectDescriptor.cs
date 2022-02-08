using Bb.ComponentModel.DataAnnotations;
using Bb.ComponentModel.Translations;
using Bb.CustomComponents.PropertyGridComponent;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bb.CustomComponents
{

    public class PropertyObjectDescriptor
    {

        static PropertyObjectDescriptor()
        {

            _strategies = new Dictionary<Type, StrategyEditor>();

            _strategies.Add(typeof(char), new StrategyEditor(PropertyKingView.Char, typeof(ComponentChar), () => ' '));
            _strategies.Add(typeof(string), new StrategyEditor(PropertyKingView.String, typeof(ComponentString), () => string.Empty));
            _strategies.Add(typeof(bool), new StrategyEditor(PropertyKingView.Bool, typeof(ComponentBool), () => true));
            _strategies.Add(typeof(bool?), new StrategyEditor(PropertyKingView.Bool, typeof(ComponentBool), () => true));

            _strategies.Add(typeof(Int16), new StrategyEditor(PropertyKingView.Int16, typeof(ComponentInt16), () => 0));
            _strategies.Add(typeof(Int16?), new StrategyEditor(PropertyKingView.Int16, typeof(ComponentInt16), () => 0));

            _strategies.Add(typeof(Int32), new StrategyEditor(PropertyKingView.Int32, typeof(ComponentInt32), () => 0));
            _strategies.Add(typeof(Int32?), new StrategyEditor(PropertyKingView.Int32, typeof(ComponentInt32), () => 0));

            _strategies.Add(typeof(Int64), new StrategyEditor(PropertyKingView.Int64, typeof(ComponentInt64), () => 0));
            _strategies.Add(typeof(Int64?), new StrategyEditor(PropertyKingView.Int64, typeof(ComponentInt64), () => 0));

            _strategies.Add(typeof(UInt16), new StrategyEditor(PropertyKingView.UInt16, typeof(ComponentUInt16), () => 0));
            _strategies.Add(typeof(UInt16?), new StrategyEditor(PropertyKingView.UInt16, typeof(ComponentUInt16), () => 0));

            _strategies.Add(typeof(UInt32), new StrategyEditor(PropertyKingView.UInt32, typeof(ComponentUInt32), () => 0));
            _strategies.Add(typeof(UInt32?), new StrategyEditor(PropertyKingView.UInt32, typeof(ComponentInt32), () => 0));

            _strategies.Add(typeof(UInt64), new StrategyEditor(PropertyKingView.UInt64, typeof(ComponentUInt64), () => 0));
            _strategies.Add(typeof(UInt64?), new StrategyEditor(PropertyKingView.UInt64, typeof(ComponentInt64), () => 0));

            _strategies.Add(typeof(DateTime), new StrategyEditor(PropertyKingView.Date, typeof(ComponentDate), () => DateTime.UtcNow));
            _strategies.Add(typeof(DateTime?), new StrategyEditor(PropertyKingView.Date, typeof(ComponentDate), () => DateTime.UtcNow));

            _strategies.Add(typeof(DateTimeOffset), new StrategyEditor(PropertyKingView.DateOffset, typeof(ComponentDateOffset), () => DateTimeOffset.UtcNow));
            _strategies.Add(typeof(DateTimeOffset?), new StrategyEditor(PropertyKingView.DateOffset, typeof(ComponentDateOffset), () => DateTimeOffset.UtcNow));

            _strategies.Add(typeof(TimeSpan), new StrategyEditor(PropertyKingView.Time, typeof(ComponentTime), () => TimeSpan.FromMinutes(0)));
            _strategies.Add(typeof(TimeSpan?), new StrategyEditor(PropertyKingView.Time, typeof(ComponentTime), () => TimeSpan.FromMinutes(0)));

            _strategies.Add(typeof(float), new StrategyEditor(PropertyKingView.Float, typeof(ComponentFloat), () => 0f));
            _strategies.Add(typeof(float?), new StrategyEditor(PropertyKingView.Float, typeof(ComponentFloat), () => 0f));

            _strategies.Add(typeof(double), new StrategyEditor(PropertyKingView.Double, typeof(ComponentDouble), () => 0d));
            _strategies.Add(typeof(double?), new StrategyEditor(PropertyKingView.Double, typeof(ComponentDouble), () => 0d));

            _strategies.Add(typeof(decimal), new StrategyEditor(PropertyKingView.Decimal, typeof(ComponentDecimal), () => 0));
            _strategies.Add(typeof(decimal?), new StrategyEditor(PropertyKingView.Decimal, typeof(ComponentDecimal), () => 0));





        }

        public static bool Create(Type type, out object? result)
        {

            result = null;

            if (_strategies.TryGetValue(type, out var strategies))
            {
                result = strategies.CreateInstance();
                return true;
            }

            return false;

        }

        public PropertyObjectDescriptor(PropertyDescriptor property, ObjectDescriptor parent)
        {

            this.Parameters = new Dictionary<string, object>();
            this.Parameters.Add("Property", this);

            this.Parent = parent;
            this.PropertyDescriptor = property;

            this.Display = property.DisplayName.GetTranslation(property.Name);
            this.Description = property.Description;
            this.Category = property.Category.GetTranslation();

            this.Browsable = true;
            this.ReadOnly = false;
            this.DefaultValue = null;
            this.Minimum = Int32.MinValue;
            this.Maximum = Int32.MaxValue;
            this.Step = 1;
            this._constraints = new List<ValidationAttribute>();
            this.Type = PropertyDescriptor.PropertyType;
            this.Line = 1;


            if (this.Type.IsGenericType && this.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                this.IsNullable = true;
                this.SubType = Type.GetGenericArguments()[0];
            }
            else
                this.SubType = typeof(void);

            if (_strategies.TryGetValue(this.Type, out StrategyEditor? strategy))
                AssignStrategy(strategy);

            else if (this.Type.IsEnum)
            {
                this.EditorType = typeof(ComponentEnumeration);
                this.KingView = PropertyKingView.Enumeration;
                this.ListProvider = typeof(EnumListProvider);
            }

            else if (typeof(IEnumerable).IsAssignableFrom(this.Type))
                foreach (var item in this.Type.GetInterfaces())
                    if (item.IsGenericType && item.GetGenericTypeDefinition() is Type type && type == typeof(ICollection<>))
                    {
                        this.SubType = item.GetGenericArguments()[0];
                        this.KingView = PropertyKingView.List;
                        this.EditorType = typeof(ComponentList);
                    }

            this.IsValid = this.EditorType != null;

        }


        public object? Value
        {
            get
            {

                object result = PropertyDescriptor.GetValue(Parent.Instance);

                if (result == null)
                    return this.DefaultValue;

                return result;

            }

            set
            {
                PropertyDescriptor.SetValue(Parent.Instance, value);
                PropertyHasChanged();
            }

        }

        public void PropertyHasChanged()
        {
            this.Parent.HasChanged(this);
        }

        internal void AnalyzeAttributes()
        {

            var attributes = PropertyDescriptor.Attributes.OfType<Attribute>().ToList();
            StrategyEditor? strategy;

            foreach (Attribute attribute in attributes)
            {

                if (attribute.GetType().Namespace != "System.Runtime.CompilerServices")

                    switch (attribute)
                    {

                        case DataTypeAttribute dataTypeAttribute:
                            switch (dataTypeAttribute.DataType)
                            {
                                
                                case DataType.DateTime:
                                    AssignStrategy(_strategies[typeof(DateTime)]);
                                    
                                    break;

                                case DataType.Date:
                                    AssignStrategy(_strategies[typeof(DateTime)]);
                                    break;

                                case DataType.Time:
                                    AssignStrategy(_strategies[typeof(TimeSpan)]);
                                    break;

                                case DataType.Password:
                                    IsPassword = true;
                                    this.EditorType = typeof(ComponentPassword);
                                    break;

                                case DataType.Duration:
                                    break;
                                case DataType.PhoneNumber:
                                    break;
                                case DataType.Currency:
                                    break;
                                case DataType.Html:
                                    break;
                                case DataType.MultilineText:
                                    Line = 5;
                                    break;
                                case DataType.EmailAddress:
                                    break;
                                case DataType.Url:
                                    break;
                                case DataType.ImageUrl:
                                    break;
                                case DataType.CreditCard:
                                    break;
                                case DataType.PostalCode:
                                    break;
                                case DataType.Upload:
                                    break;
                                case DataType.Custom:
                                    break;
                                default:
                                    break;

                            }
                            break;

                        case DisplayOnUITextAreaAttribute:
                            this.Line = 3;
                            break;

                        case ListProviderAttribute listProviderAttribute:
                            this.ListProvider = listProviderAttribute.EnumerationResolver;
                            this.EditorType = typeof(ComponentEnumeration);
                            this.KingView = PropertyKingView.Enumeration;
                            break;

                        case EditorAttribute editor:
                            this.EditorType = Type.GetType(editor.EditorTypeName);
                            break;

                        case CategoryAttribute:
                        case DisplayNameAttribute:
                        case DescriptionAttribute:
                            break;

                        case PasswordPropertyTextAttribute passwordPropertyText:
                            IsPassword = passwordPropertyText.Password;
                            this.EditorType = typeof(ComponentPassword);
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

                        case Bb.ComponentModel.DataAnnotations.TranslationKeyAttribute translationKey:
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

        private void AssignStrategy(StrategyEditor strategy)
        {
            this.EditorType = strategy.ComponentView;
            this.KingView = strategy.PropertyKingView;
        }

        public bool Validate(object value, List<string> messages)
        {

            foreach (var item in _constraints)
                if (!item.IsValid(value))
                {

                    var label = this.Parent.TranslateService.Translate(this.Display);
                    var message = item.FormatErrorMessage(label);

                    if (item.FormatErrorMessage(string.Empty).IsValidTranslationKey())
                        messages.Add(this.Parent.TranslateService.Translate(message));
                    else
                        messages.Add(message);

                }

            return messages.Count == 0;

        }


        public Type Type { get; internal set; }

        public IDictionary<string, object> Parameters { get; set; }


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

        public Type? EditorType { get; private set; }

        public Type ListProvider { get; private set; }

        public PropertyDescriptor PropertyDescriptor { get; set; }

        public bool IsValid { get; }
        public int Line { get; private set; }

        private readonly List<ValidationAttribute> _constraints;
        private static Dictionary<Type, StrategyEditor> _strategies;

    }


    public class StrategyEditor
    {

        public StrategyEditor(PropertyKingView propertyKingView, Type componentView, Func<object> createInstance)
        {
            this.PropertyKingView = propertyKingView;
            this.ComponentView = componentView;
            this.CreateInstance = createInstance;
        }

        public PropertyKingView PropertyKingView { get; }

        public Type ComponentView { get; }

        public Func<object> CreateInstance { get; }


    }

    public class EnumListProvider : IListProvider
    {

        public EnumListProvider()
        {

        }


        public PropertyDescriptor Property { get; set; }


        public ITranslateService TranslateService { get; set; }


        public IEnumerable<ListItem> GetItems()
        {

            var values = Enum.GetValues(Property.PropertyType);
            var fields = Property.PropertyType.GetFields();

            foreach (var item in values)
            {

                var n = item.ToString();
                var o = fields.Where(f => f.Name == n).First();
                TranslatedKeyLabel label = o.GetFrom().FirstOrDefault() ?? n;

                yield return new ListItem()
                {
                    Name = n,
                    Value = item,
                    Display = TranslateService.Translate(label),
                };

            }


        }


    }

}
