using CQRS.Application.Repositories;
using CQRS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Commands.Orders.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommandRequest, AddOrderCommandResponse>
    {
        private readonly IReadRepository<Order> _orderReadRepository;
        private readonly IWriteRepository<Order> _orderWriteRepository;

        public AddOrderCommandHandler(IReadRepository<Order> orderReadRepository, IWriteRepository<Order> orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<AddOrderCommandResponse> Handle(AddOrderCommandRequest request, CancellationToken cancellationToken)
        {
            Order order = new();
            order.Number = request.Number;
            order.ProviderId = request.ProviderId;
            order.OrderItemId = request.OrderItemId;

            if (_orderReadRepository.GetWhere(o => o.Number == order.Number && o.ProviderId == order.ProviderId).Any())
                return new()
                {
                    success = false,
                    message = "There already exists same order with this number and provider."
                };

            var response = await _orderWriteRepository.AddAsync(order);

            if (!response)
                return new()
                {
                    success = false,
                    message = "Error happened when trying to add order. Please try again."
                };

            await _orderWriteRepository.SaveChangesAsync();

            return new()
            {
                success = true
            };
        }
    }
}
