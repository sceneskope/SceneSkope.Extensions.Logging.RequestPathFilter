using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SceneSkope.Extensions.Logging.RequestPathFilter.Tests
{
    public class TestState : IEnumerable<KeyValuePair<string, object>>
    {
        public string RequestPath { get; }
        public TestState(string requestPath)
        {
            RequestPath = requestPath;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            yield return new KeyValuePair<string, object>("RequestPath", RequestPath);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
