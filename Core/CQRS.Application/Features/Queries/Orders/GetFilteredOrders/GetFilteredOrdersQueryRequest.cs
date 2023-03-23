using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.Orders.GetFilteredOrders
{
    public class GetFilteredOrdersQueryRequest : IRequest<GetFilteredOrdersQueryResponse>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
