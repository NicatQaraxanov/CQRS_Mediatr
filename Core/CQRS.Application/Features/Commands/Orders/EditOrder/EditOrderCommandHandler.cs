using CQRS.Application.Repositories;
using CQRS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Commands.Orders.EditOrder
{
    public class EditOrderCommandHandler : IRequestHandler<EditOrderCommandRequest, EditOrderCommandResponse>
    {
        private readonly IReadRepository<Order> _orderReadRepository;
        private readonly IReadRepository<Provider> _providerReadRepository;
        private readonly IReadRepository<OrderItem> _orderItemReadRepository;
        private readonly IWriteRepository<Order> _orderWriteRepository;

        public EditOrderCommandHandler(IReadRepository<Order> orderReadRepository, IReadRepository<Provider> providerReadRepository, IReadRepository<OrderItem> orderItemReadRepository, IWriteRepository<Order> orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _providerReadRepository = providerReadRepository;
            _orderItemReadRepository = orderItemReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<EditOrderCommandResponse> Handle(EditOrderCommandRequest request, CancellationToken cancellationToken)
        {
            Order order = new Order
            {
                Id = request.Id,
                Number = request.Number,
                OrderItemId = request.OrderItemId,
                ProviderId = request.ProviderId
            };
            order.OrderItem = await _orderItemReadRepository.Get(order.OrderItemId);
            order.Provider = await _providerReadRepository.Get(order.ProviderId);

            if (_orderReadRepository.GetWhere(o => o.Number == order.Number && o.ProviderId == order.ProviderId).Any())
                return new()
                {
                    success = false,
                    message = "There already exists same order with this number and provider."
                };

            var response = _orderWriteRepository.Update(order);

            if (response == false)
                return new()
                {
                    success = false,
                    message = "Error happened when trying to edit order. Please try again."
                };

            await _orderWriteRepository.SaveChangesAsync();

            return new() { success = true };
        }
    }
}
