using Microsoft.Extensions.Primitives;

namespace Bb.Storages.ConfigurationProviders.SqlServer
{

    public interface ISqlServerWatcher : IDisposable
    {
        IChangeToken Watch();

    }

}