using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayCatelogServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new List<ItemDto>()
        {
            new ItemDto(Guid.NewGuid(),"Pen","Red Color Pen",10,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Book","Sample books",300,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Showes","small showes",560,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Keyboard","Mini Keyboard",550,DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        [HttpGet("{id}")]
        public ItemDto GetItemById(Guid id)
        {
            var item = items.Where(x => x.Id == id).SingleOrDefault();
            return item;
        }


        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public ActionResult<ItemDto> Put(Guid id, UpdateItemDto updateItemDto)
        {
            var item = items.Where(x => x.Id == id).SingleOrDefault();

            var updateItem = item with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price,
            };

            var index = items.FindIndex(x => x.Id == id);
            items[index] = updateItem;
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = items.FindIndex(x => x.Id == id);
            items.RemoveAt(index);
            return NoContent();
        }


    }
}
