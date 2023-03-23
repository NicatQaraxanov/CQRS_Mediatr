using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Commands.Orders.EditOrder
{
    public class EditOrderCommandResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}
