using PlayCatelogServiceApp.Entities;
using System;

namespace PlayCatelogServiceApp.Helpers
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }

        public static Item AsCreateModel(this CreateItemDto item)
        {
            return new Item()
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CreatedDate = DateTime.UtcNow
            };
        }

        public static Item AsUpdateModel(this UpdateItemDto item, Guid id)
        {
            return new Item()
            {
                Id = id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CreatedDate = DateTime.UtcNow
            };
        }

    }
}
