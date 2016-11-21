using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using SceneSkope.Extensions.Logging.RequestPathFilter.Internal;

namespace SceneSkope.Extensions.Logging.RequestPathFilter
{
    public static class RequestPathFilterLoggerFactoryExtensions
    {
        public static ILoggerFactory WithRequestPathFilter(this ILoggerFactory factory, string requestPathToIgnore) =>
            new RequestPathFilterLoggerFactory(factory, requestPathToIgnore);
    }
}
