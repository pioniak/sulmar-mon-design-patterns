using MediatorPattern.Events;
using MediatorPattern.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPattern.Handlers
{
    public class AddDbCustomerHandler : INotificationHandler<AddCustomerEvent>
    {
        private readonly ICustomerRepository customerRepository;

        public AddDbCustomerHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task Handle(AddCustomerEvent @event, CancellationToken cancellationToken)
        {
            // return Task.Run(()=>customerRepository.Add(@event.Customer));

            await Task.Delay(TimeSpan.FromSeconds(5));

            customerRepository.Add(@event.Customer);

           // throw new Exception();

            // return Task.CompletedTask;

        }
    }
}
