using System;

namespace Bb.WebHost.Startings
{

    public class ExposedType
    {

        public ExposedType(Type typeModel, string context, Func<IServiceProvider, object> func)
        {
            Type = typeModel;
            Context = context;
            Function = func;
        }

        public Type Type { get; }

        public string Context { get; }

        public Func<IServiceProvider, object> Function { get; }

    }

}
