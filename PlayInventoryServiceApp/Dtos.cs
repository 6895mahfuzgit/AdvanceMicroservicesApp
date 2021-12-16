using System;

namespace PlayInventoryServiceApp
{
    public record GrandItemDto(Guid UserId, Guid CatagoryId, int Quantity);
    public record InventoryItemDto(Guid CatagoryId, int Quantity, DateTimeOffset AcquiredDate);

    public record CatalogItemDto(Guid Id, string Name, string Description);

}
