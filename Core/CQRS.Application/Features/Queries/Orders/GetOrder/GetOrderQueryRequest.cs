using CQRS.Application.Features.Queries.Orders.GetAllOrders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.Orders.GetOrder
{
    public class GetOrderQueryRequest : IRequest<GetOrderQueryResponse>
    {
        public int Id { get; set; }
    }
}
