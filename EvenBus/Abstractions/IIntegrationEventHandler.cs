using EvenBus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvenBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrtionEvent> : IIntegrationEventHandler 
        where TIntegrtionEvent : IntegrationEvent
    {
        Task Handle(TIntegrtionEvent @event);
    }

    public interface IIntegrationEventHandler
    {

    }
}
