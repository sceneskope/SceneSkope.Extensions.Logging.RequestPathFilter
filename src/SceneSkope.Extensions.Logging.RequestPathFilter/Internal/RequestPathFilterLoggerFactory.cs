using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SceneSkope.Extensions.Logging.RequestPathFilter.Internal
{
    internal class RequestPathFilterLoggerFactory : ILoggerFactory
    {
        private readonly ILoggerFactory _wrappedFactory;
        private readonly string _requestPathToIgnore;

        public RequestPathFilterLoggerFactory(ILoggerFactory factory, string requestPathToIgnore)
        {
            _wrappedFactory = factory;
            _requestPathToIgnore = requestPathToIgnore;

        }
        public void AddProvider(ILoggerProvider provider)
        {
            var wrappedProvider = new RequestPathFilterLoggerProvider(provider, _requestPathToIgnore);
            _wrappedFactory.AddProvider(wrappedProvider);
        }

        public ILogger CreateLogger(string categoryName) => _wrappedFactory.CreateLogger(categoryName);

        public void Dispose()
        {
            // Nothing to dispose here - dispose should be called on the wrapped factory
        }
    }
}
