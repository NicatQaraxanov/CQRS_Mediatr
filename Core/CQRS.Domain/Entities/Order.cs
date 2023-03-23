using CQRS.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            Date = DateTime.Now;
        }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int ProviderId { get; set; }

        public Provider Provider { get; set; }

        public int OrderItemId { get; set; }

        public OrderItem OrderItem { get; set; }
    }
}
