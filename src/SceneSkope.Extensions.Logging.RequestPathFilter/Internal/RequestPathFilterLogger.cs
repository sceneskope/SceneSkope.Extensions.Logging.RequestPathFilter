using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SceneSkope.Extensions.Logging.RequestPathFilter.Internal
{
    internal class RequestPathFilterLogger : ILogger
    {
        private readonly RequestPathFilterLoggerProvider _provider;
        private readonly string _categoryName;
        private readonly ILogger _logger;
        private readonly string _requestPathToIgnore;

        public RequestPathFilterLogger(RequestPathFilterLoggerProvider provider, ILogger logger, string categoryName, string requestPathToIgnore)
        {
            _provider = provider;
            _logger = logger;
            _categoryName = categoryName;
            _requestPathToIgnore = requestPathToIgnore;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            var wrapped = _logger.BeginScope(state);
            return _provider.BeginScope(state, wrapped);
        }

        public bool IsEnabled(LogLevel logLevel) => _logger.IsEnabled(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!_provider.RequestPathMatched && !_provider.DoesRequestPathMatch(state))
            {
                _logger.Log(logLevel, eventId, state, exception, formatter);
            }
        }

    }
}