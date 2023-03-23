using CQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.OrderItems.GetAllOrderItems
{
    public class GetOrderItemsQueryResponse
    {
        public List<OrderItem> OrderItems { get; set; }
    }
}
