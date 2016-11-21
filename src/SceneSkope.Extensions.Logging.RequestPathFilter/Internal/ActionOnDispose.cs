using System;
using Microsoft.Extensions.Logging;

namespace SceneSkope.Extensions.Logging.RequestPathFilter.Internal
{
    internal class ActionOnDispose : IDisposable
    {
        private readonly Action _disposer;
        public ActionOnDispose(Action disposer)
        {
            _disposer = disposer;
        }

        public void Dispose()
        {
            _disposer();
        }
    }
}