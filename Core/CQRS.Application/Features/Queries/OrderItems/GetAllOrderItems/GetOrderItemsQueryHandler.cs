using CQRS.Application.Repositories;
using CQRS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.OrderItems.GetAllOrderItems
{
    public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQueryRequest, GetOrderItemsQueryResponse>
    {

        private readonly IReadRepository<OrderItem> _orderItemReadRepository;

        public GetOrderItemsQueryHandler(IReadRepository<OrderItem> orderItemReadRepository)
        {
            _orderItemReadRepository = orderItemReadRepository;
        }

        public async Task<GetOrderItemsQueryResponse> Handle(GetOrderItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var orderItems = _orderItemReadRepository.GetAll().ToList();

            return new()
            {
                OrderItems = orderItems
            };
        }
    }
}
