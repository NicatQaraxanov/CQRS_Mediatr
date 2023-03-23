using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Commands.Orders.DeleteOrder
{
    public class DeleteOrderCommandResponse
    {
        public bool success { get; set; }
    }
}
