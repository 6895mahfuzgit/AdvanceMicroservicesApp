using System;

namespace PlayInventoryServiceApp
{
    public record GrandItemDto(Guid UserId, Guid CatagoryId, int Quantity);
    public record InventoryItemDto(Guid CatagoryId, string Name, string Description, int Quantity, DateTimeOffset AcquiredDate);
    public record CatalogItemDto(Guid Id, string Name, string Description);

}
