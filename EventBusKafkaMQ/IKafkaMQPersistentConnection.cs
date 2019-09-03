using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusKafkaMQ
{
    public interface IKafkaMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();
        //IModel CreateModel();
    }
}
