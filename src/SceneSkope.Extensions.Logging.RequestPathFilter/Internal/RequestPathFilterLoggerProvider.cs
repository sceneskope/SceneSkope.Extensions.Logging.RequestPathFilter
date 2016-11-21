using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
#if ASYNCLOCAL
using System.Threading;
#else
using System.Runtime.Remoting.Messaging;
#endif

namespace SceneSkope.Extensions.Logging.RequestPathFilter.Internal
{
    internal class RequestPathFilterLoggerProvider : ILoggerProvider
    {
        private readonly ILoggerProvider _provider;
        private readonly string _requestPathToIgnore;

        public RequestPathFilterLoggerProvider(ILoggerProvider provider, string requestPathToIgnore)
        {
            _provider = provider;
            _requestPathToIgnore = requestPathToIgnore;
        }

        public ILogger CreateLogger(string categoryName)
        {
            var logger = _provider.CreateLogger(categoryName);
            var wrapped = new RequestPathFilterLogger(this, logger, categoryName, _requestPathToIgnore);
            return wrapped;
        }

#if ASYNCLOCAL
        private AsyncLocal<bool> _requestPathMatched = new AsyncLocal<bool>();
        public bool RequestPathMatched {
            get {
                return _requestPathMatched.Value;
            }
            set {
                _requestPathMatched.Value = value;
            }
        }
#else
        private readonly string _scopeKey = nameof(RequestPathFilterLoggerProvider) + "#" + Guid.NewGuid().ToString("N");
        public bool RequestPathMatched
        {
            get
            {
                var value = CallContext.LogicalGetData(_scopeKey);
                return (value != null) && (bool)value;
            }
            set
            {
                CallContext.LogicalSetData(_scopeKey, value);
            }
        }

#endif


        public IDisposable BeginScope<TState>(TState state, IDisposable wrapped)
        {
            if (RequestPathMatched)
            {
                return wrapped;
            }
            else
            {
                var matched = DoesRequestPathMatch(state);
                if (!matched)
                {
                    return wrapped;
                }
                else
                {
                    RequestPathMatched = true;
                    return new ActionOnDispose(() =>
                    {
                        RequestPathMatched = false;
                        wrapped.Dispose();
                    });

                }
            }
        }

        public bool DoesRequestPathMatch<TState>(TState state)
        {
            var properties = state as IEnumerable<KeyValuePair<string, object>>;
            if (properties != null)
            {
                foreach (var property in properties)
                {
                    if (property.Key == "RequestPath")
                    {
                        if (((string)property.Value).Equals(_requestPathToIgnore, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        public void Dispose() => _provider.Dispose();
    }
}