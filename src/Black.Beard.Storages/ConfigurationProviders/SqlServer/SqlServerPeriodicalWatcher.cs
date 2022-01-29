﻿using Microsoft.Extensions.Primitives;


namespace Bb.Storages.ConfigurationProviders.SqlServer
{

    internal class SqlServerPeriodicalWatcher : ISqlServerWatcher
    {
        

        public SqlServerPeriodicalWatcher(TimeSpan refreshInterval)
        {
            _refreshInterval = refreshInterval;
            _timer = new Timer(Change, null, TimeSpan.Zero, _refreshInterval);
        }

        private void Change(object state)
        {
            _cancellationTokenSource?.Cancel();
        }

        public IChangeToken Watch()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _changeToken = new CancellationChangeToken(_cancellationTokenSource.Token);

            return _changeToken;

        }

        public void Dispose()
        {
            _timer?.Dispose();
            _cancellationTokenSource?.Dispose();
        }

        private readonly TimeSpan _refreshInterval;
        private IChangeToken _changeToken;
        private readonly Timer _timer;
        private CancellationTokenSource _cancellationTokenSource;

    }
}