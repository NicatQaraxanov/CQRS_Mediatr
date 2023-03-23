using CQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.Orders.GetAllOrders
{
    public class GetOrdersQueryResponse
    {
        public List<Order> Orders { get; set; }
    }
}
