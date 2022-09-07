using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int? Userid { get; set; }
        public int? Quantity { get; set; }
        public DateTime? Orderdate { get; set; }
    }
}
