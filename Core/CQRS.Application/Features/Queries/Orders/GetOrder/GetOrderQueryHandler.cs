using CQRS.Application.Repositories;
using CQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.Orders.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQueryRequest, GetOrderQueryResponse>
    {
        private readonly IReadRepository<Order> _orderReadRepository;

        public GetOrderQueryHandler(IReadRepository<Order> repository)
        {
            _orderReadRepository = repository;
        }

        public async Task<GetOrderQueryResponse> Handle(GetOrderQueryRequest request, CancellationToken cancellationToken)
        {
            Order order = _orderReadRepository.GetAll().Include(o => o.Provider).Include(o => o.OrderItem).FirstOrDefault(o => o.Id == request.Id);

            if(order == null)
            {
                return null;
            }

            return new()
            {
                Order = order
            };
        }
    }
}
