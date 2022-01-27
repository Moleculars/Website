using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bb.MolecularSite.PropertyGridComponent
{

    public class PropertyObjectDescriptor
    {

        public PropertyObjectDescriptor(PropertyDescriptor property)
        {
            this._property = property;
            this.Display = property.Name;
            this._constraints = new List<ValidationAttribute>();
            this.Type = _property.PropertyType;
        }


        internal void Analyze()
        {

            foreach (Attribute attribute in _property.Attributes)
            {
                if (attribute.GetType().Namespace != "System.Runtime.CompilerServices")

                    switch (attribute)
                    {

                        case DisplayNameAttribute display:
                            this.Display = display.DisplayName;
                            break;

                        case DescriptionAttribute description:
                            this.Description = description.Description;
                            break;

                        case CategoryAttribute category:
                            this.Category = category.Category;
                            break;

                        case System.ComponentModel.BrowsableAttribute browsable:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                        case System.ComponentModel.DefaultValueAttribute defaultValue:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                        case System.ComponentModel.EditorAttribute editor:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                        case System.ComponentModel.PropertyTabAttribute propertyTab:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                        case System.ComponentModel.ReadOnlyAttribute readOnly:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                        case System.ComponentModel.TypeConverterAttribute typeConverter:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                        case System.ComponentModel.TypeDescriptionProviderAttribute typeDescriptionProvider:
                            if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();
                            break;

                            case Bb.ComponentModel.Attributes.TranslationKeyAttribute translationKey:

                            break;

                        default:

                            if (attribute is ValidationAttribute validation)
                                this._constraints.Add(validation);

                            else
                                if (System.Diagnostics.Debugger.IsAttached)
                                System.Diagnostics.Debugger.Break();

                            break;
                    }


            }

        }

        public string Display { get; private set; }

        private readonly List<ValidationAttribute> _constraints;

        public Type Type { get; }
        public string Description { get; private set; }
        public string Category { get; private set; }

        private readonly PropertyDescriptor _property;


    }


}
