using System;
using System.Collections.Concurrent;
using System.Threading;

using VB6DotNet.Runtime.Hosting;

namespace VB6DotNet.Runtime.Threading
{

    /// <summary>
    /// Provides a container for a VB6 components execution lifetime.
    /// </summary>
    public abstract class Apartment : IDisposable
    {

        readonly Host host;
        readonly CancellationTokenSource cts = new CancellationTokenSource();
        readonly ConcurrentDictionary<RuntimeObjectType, RuntimeRemoteProxyType> proxyTypes = new ConcurrentDictionary<RuntimeObjectType, RuntimeRemoteProxyType>();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="host"></param>
        protected Apartment(Host host)
        {
            this.host = host ?? throw new ArgumentNullException(nameof(host));
        }

        /// <summary>
        /// Gets the cancellation token that signals the shutdown of the apartment.
        /// </summary>
        public CancellationToken CancellationToken => cts.Token;

        /// <summary>
        /// Dispatches an action to run on the apartment thread.
        /// </summary>
        /// <param name="action"></param>
        public abstract void Dispatch(Action<CancellationToken> action);

        /// <summary>
        /// Gets a <see cref="RuntimeRemoteProxyType"/> that servies the specified <see cref="RuntimeObjectType"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public RuntimeRemoteProxyType GetProxyType(RuntimeObjectType type)
        {
            return proxyTypes.GetOrAdd(type, _ => new RuntimeRemoteProxyType(_));
        }

        /// <summary>
        /// Disposes of the instance.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (cts.IsCancellationRequested == false)
                cts.Cancel();
        }

        /// <summary>
        /// Disposes of the instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes the instance.
        /// </summary>
        ~Apartment()
        {
            Dispose(false);
        }

    }

}
