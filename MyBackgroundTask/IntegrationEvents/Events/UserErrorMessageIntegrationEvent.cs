using EvenBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundTask.IntegrationEvents.Events
{
    public class UserErrorMessageIntegrationEvent : IntegrationEvent
    {
        public string PayName { get; set; }
        public string PayType { get; set; }
        public string PayCode { get; set; }
        public string Error { get; set; }
        public DateTime GetTime { get; set; }

        public UserErrorMessageIntegrationEvent(string payName, string payType, string payCode, string error, DateTime getTime)
        {
            PayName = payName;
            PayType = payType;
            PayCode = payCode;
            Error = error;
            GetTime = getTime;

        }
    }
}
