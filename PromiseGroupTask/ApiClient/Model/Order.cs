using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Model
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

    }
}
