using CQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.Orders.GetOrder
{
    public class GetOrderQueryResponse
    {
        public Order Order { get; set; }
    }
}
