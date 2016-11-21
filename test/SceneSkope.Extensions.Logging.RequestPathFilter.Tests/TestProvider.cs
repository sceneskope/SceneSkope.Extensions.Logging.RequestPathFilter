using System;
using Microsoft.Extensions.Logging;

namespace SceneSkope.Extensions.Logging.RequestPathFilter.Tests
{
    public class TestProvider : ILoggerProvider
    {
        public bool DisposeCalled { get; private set; }
        public TestSink Sink { get; }

        public TestProvider(TestSink sink)
        {
            Sink = sink;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TestLogger(categoryName, Sink);
        }

        public void Dispose()
        {
            DisposeCalled = true;
        }
    }
}
