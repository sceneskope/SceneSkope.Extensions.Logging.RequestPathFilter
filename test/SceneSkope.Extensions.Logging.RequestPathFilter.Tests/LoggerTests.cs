using Microsoft.Extensions.Logging;
using Xunit;

namespace SceneSkope.Extensions.Logging.RequestPathFilter.Tests
{
    public class LoggerTests
    {
        [Theory]
        [InlineData("/test", "/test", false)]
        [InlineData("/testing", "/test", true)]
        [InlineData("/test", "/testing", true)]
        public void LogsCorrectly(string requestPath, string requestPathFilter, bool shouldLog)
        {
            var factory = new LoggerFactory();
            var filteredFactory = factory.WithRequestPathFilter(requestPathFilter);
            var provider = new TestProvider(new TestSink());
            filteredFactory.AddProvider(provider);
            var logger = filteredFactory.CreateLogger("Test");

            logger.BeginScope(new TestState(requestPath));
            logger.LogInformation("Test message");

            Assert.Equal(shouldLog ? 1 : 0, provider.Sink.WriteCount);

        }
    }
}
