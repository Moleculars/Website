using System;
using System.Collections.Generic;
using System.Runtime.Loader;
using System.Threading;

namespace Bb.WebHost
{

    /// <summary>
    /// Wrap process stop application
    /// </summary>
    public class KilledGracefullInterceptor : IDisposable
    {

        #region Ctor

        static KilledGracefullInterceptor()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            AssemblyLoadContext.Default.Unloading += Default_Unloading;
            _subscriptions = new Dictionary<object, KilledGracefullInterceptor>();
        }

        internal KilledGracefullInterceptor(IDisposable service)
            : this(service, () => service.Dispose())
        {
        }

        internal KilledGracefullInterceptor(object service, Action stopAction)
            : this()
        {
            _service = service;
            StopAction = StopAction;
            _subscriptions.Add(service, this);
        }

        private KilledGracefullInterceptor()
        {
            _eventHandler += Stop_Impl;
        }

        #endregion Ctor

        #region Stop

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Stop(false);
        }

        private static void Default_Unloading(AssemblyLoadContext obj)
        {
            Stop(true);
        }

        private void Stop_Impl(object sender, EventArgs e)
        {
            StopAction?.Invoke();
            Remove();
        }

        public static void Stop()
        {
            Stop(false);
        }

        private static void Stop(bool unloading)
        {

            if (_subscriptions.Count > 0 && !_stoped)
                lock (_lock)
                    if (_subscriptions.Count > 0 && !_stoped)
                    {

                        _stoped = true;
                        _eventHandler(null, new EventArgs());

                        if (unloading)
                        {
                            var delay = DateTime.Now.AddSeconds(3);
                            while (_subscriptions.Count > 0 || delay > DateTime.Now)
                                Thread.Yield();
                        }
                    }

        }

        public static bool ApplicationStoping { get => _stoped; }


        public Action StopAction { get; set; }

        #endregion Stop

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {

                if (disposing)
                    _eventHandler -= Stop_Impl;

                disposedValue = true;

            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~KillInterceptor()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }


        public static void Append(IDisposable instance)
        {
            new KilledGracefullInterceptor(instance);
        }

        public static void Append(object service, Action stopAction)
        {
            new KilledGracefullInterceptor(service, stopAction);
        }

        private void Remove()
        {

            if (_subscriptions.ContainsKey(_service))
                lock (_lock)
                    if (_subscriptions.ContainsKey(_service))
                        _subscriptions.Remove(_service);

        }

        #endregion

        private static EventHandler<EventArgs> _eventHandler;
        private static object _lock = new object();
        private bool disposedValue = false; // To detect redundant calls
        private static bool _stoped;
        private static readonly Dictionary<object, KilledGracefullInterceptor> _subscriptions;
        private readonly object _service;

    }


}
