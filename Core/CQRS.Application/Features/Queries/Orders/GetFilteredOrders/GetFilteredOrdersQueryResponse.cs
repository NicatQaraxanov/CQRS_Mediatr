﻿using CQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.Orders.GetFilteredOrders
{
    public class GetFilteredOrdersQueryResponse
    {
        public GetFilteredOrdersQueryResponse()
        {
            Orders = new();
        }

        public List<Order> Orders { get; set; }
    }
}
