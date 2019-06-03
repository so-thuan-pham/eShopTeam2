using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Events
{
    public class OrderStartedDomainEvent : INotification
    {
        public string EmailAddress { get; }

        public OrderStartedDomainEvent(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}
