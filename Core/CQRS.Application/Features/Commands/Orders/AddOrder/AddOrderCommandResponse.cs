using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Commands.Orders.AddOrder
{
    public class AddOrderCommandResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}
