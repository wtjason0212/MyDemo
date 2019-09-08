using EvenBus.Abstractions;
using EvenBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenBus
{
    public interface IEventBusSubscriptionsManager
    {
        void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}
