using CQRS.Application.Repositories;
using CQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.Orders.GetAllOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQueryRequest, GetOrdersQueryResponse>
    {
        private readonly IReadRepository<Order> _orderReadRepository;

        public GetOrdersQueryHandler(IReadRepository<Order> repository)
        {
            _orderReadRepository = repository;
        }

        public async Task<GetOrdersQueryResponse> Handle(GetOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            List<Order> orders = new();
            orders = _orderReadRepository.GetAll().Include(o => o.Provider).Include(o => o.OrderItem).ToList();

            return new()
            {
                Orders = orders
            };
        }
    }
}
