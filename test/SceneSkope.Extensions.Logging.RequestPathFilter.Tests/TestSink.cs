using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SceneSkope.Extensions.Logging.RequestPathFilter.Tests
{
    public class TestSink
    {
        public int WriteCount { get; private set; }
        internal void Begin<TState>(string categoryName, TState state)
        {
        }

        internal void Write<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter, object _lastScope)
        {
            WriteCount += 1;
        }
    }
}
