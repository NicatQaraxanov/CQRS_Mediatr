using CQRS.Application.Repositories;
using CQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.Orders.GetFilteredOrders
{
    public class GetFilteredOrdersQueryHandler : IRequestHandler<GetFilteredOrdersQueryRequest, GetFilteredOrdersQueryResponse>
    {
        private readonly IReadRepository<Order> _orderReadRepository;

        public GetFilteredOrdersQueryHandler(IReadRepository<Order> orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<GetFilteredOrdersQueryResponse> Handle(GetFilteredOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderReadRepository.GetWhere(o => o.Date >= request.StartDate && o.Date <= request.EndDate).Include(o => o.OrderItem).Include(o => o.Provider).ToListAsync();

            return new()
            {
                Orders = orders
            };
        }
    }
}
