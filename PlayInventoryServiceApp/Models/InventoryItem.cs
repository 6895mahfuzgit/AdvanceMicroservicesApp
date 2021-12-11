using PlayCommonApp.Entities;
using System;

namespace PlayInventoryServiceApp.Models
{
    public class InventoryItem : IEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid CatagoryItemId { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset AcquiredDate { get; set; }
    }
}
