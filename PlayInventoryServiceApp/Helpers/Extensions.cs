using PlayInventoryServiceApp.Models;

namespace PlayInventoryServiceApp.Helpers
{
    public static class Extensions
    {
        public static InventoryItemDto AsDto(this InventoryItem inventoryItem)
        {
            return new InventoryItemDto(inventoryItem.CatagoryItemId, inventoryItem.Quantity, inventoryItem.AcquiredDate);
        }
    }
}
