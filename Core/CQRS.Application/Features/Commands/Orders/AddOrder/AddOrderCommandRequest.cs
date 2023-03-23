using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Commands.Orders.AddOrder
{
    public class AddOrderCommandRequest : IRequest<AddOrderCommandResponse>
    {
        public AddOrderCommandRequest()
        {
            Providers = new();
            OrderItems = new();
        }

        [Required]
        public string Number { get; set; }

        [DisplayName("Provider")]
        public int ProviderId { get; set; }

        [DisplayName("Order Items")]
        public int OrderItemId { get; set; }
        
        public List<SelectListItem> Providers { get; set; }

        public List<SelectListItem> OrderItems { get; set; }
    }
}
