using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvenBus.Abstractions;
using EvenBus.Events;

namespace EvenBus
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {

        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            DoAddSubscription(typeof(TH), eventName, isDynamic: false);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            if (isDynamic)
            {
                _handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
            }
            else
            {
                _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
            }
        }

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }
    }
}
