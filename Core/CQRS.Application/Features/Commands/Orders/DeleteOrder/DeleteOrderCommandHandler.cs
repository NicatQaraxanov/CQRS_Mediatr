using CQRS.Application.Repositories;
using CQRS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Commands.Orders.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest, DeleteOrderCommandResponse>
    {
        private readonly IWriteRepository<Order> _orderWriteRepository;

        public DeleteOrderCommandHandler(IWriteRepository<Order> orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var response = _orderWriteRepository.Remove(request.Order);

            if (response == false)
                return new() { success = false };

            await _orderWriteRepository.SaveChangesAsync();

            return new() { success = true };
        }
    }
}
