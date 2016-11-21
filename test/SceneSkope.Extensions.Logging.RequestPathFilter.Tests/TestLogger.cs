using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SceneSkope.Extensions.Logging.RequestPathFilter.Tests
{
    public class TestLogger : ILogger
    {
        private object _lastScope;

        private string CategoryName { get; }
        private TestSink Sink { get; }

        public TestLogger(string categoryName, TestSink sink)
        {
            CategoryName = categoryName;
            Sink = sink;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            _lastScope = state;
            Sink.Begin(CategoryName, state);
            return NoopDisposable.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Sink.Write(logLevel, eventId, state, exception, formatter, _lastScope);
        }

        private class NoopDisposable : IDisposable
        {
            public static readonly NoopDisposable Instance = new NoopDisposable();
            public void Dispose() { }
        }
    }
}
