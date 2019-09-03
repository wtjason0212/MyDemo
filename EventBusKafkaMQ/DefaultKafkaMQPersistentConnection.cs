using Microsoft.Extensions.Logging;
using System;

namespace EventBusKafkaMQ
{
    public class DefaultKafkaMQPersistentConnection : IKafkaMQPersistentConnection
    {
        private readonly ILogger<DefaultKafkaMQPersistentConnection> _logger;
        public bool IsConnected
        {
            get;
        }
        public DefaultKafkaMQPersistentConnection(ILogger<DefaultKafkaMQPersistentConnection> logger)
        {
            _logger = logger;
        }

    
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool TryConnect()
        {
            throw new NotImplementedException();
        }
    }
}
