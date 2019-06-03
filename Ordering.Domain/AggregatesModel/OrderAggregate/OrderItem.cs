using System;

namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class OrderItem : Entity
    {
        //public virtual int ProductId { get; set; }
        public virtual string SKU { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual int Quantity { get; set; }

        protected OrderItem() { }

        public OrderItem(string sku, decimal unitPrice, int quantity = 1)
        {
            SKU = sku;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public virtual void InscreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
}
