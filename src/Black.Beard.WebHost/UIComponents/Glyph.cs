using Bb.WebHost.UIComponents.Glyphs;

namespace Bb.WebHost.UIComponents
{
    public struct Glyph
    {

        public Glyph(string value)
        {

            this.Value = value;

        }

        public string Value { get; private set; }

        public static Glyph Empty { get; } = new Glyph(string.Empty);
           

    }

}


