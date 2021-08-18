namespace Sample.App.Order.Data.Models
{
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }

        public List<OrderLine> Lines { get; set; } = new();
    }
}
