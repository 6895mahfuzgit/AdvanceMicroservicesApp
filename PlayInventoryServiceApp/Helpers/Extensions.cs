﻿using PlayInventoryServiceApp.Models;

namespace PlayInventoryServiceApp.Helpers
{
    public static class Extensions
    {
        public static InventoryItemDto AsDto(this InventoryItem inventoryItem)
        {
            return new InventoryItemDto(inventoryItem.CatagoryItemId, inventoryItem.Quantity, inventoryItem.AcquiredDate);
        }

        public static InventoryItem AsModel(this GrandItemDto grandItemDto)
        {
            return new InventoryItem
            {
                CatagoryItemId = grandItemDto.CatagoryId,
                UserId = grandItemDto.UserId,
                Quantity = grandItemDto.Quantity,
                AcquiredDate = System.DateTimeOffset.UtcNow
            };
        }
    }
}
