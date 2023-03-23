using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.OrderItems.GetAllOrderItems
{
    public class GetOrderItemsQueryRequest : IRequest<GetOrderItemsQueryResponse>
    {
    }
}
