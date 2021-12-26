using System;
namespace PlayCategoryContractsApp
{
    public record CatalogItemCreated(Guid ItemId, string Name, string Description);
    public record CatalogItemUpdated(Guid ItemId, string Name, string Description);
    public record CatalogItemDelete(Guid ItemId);
}
