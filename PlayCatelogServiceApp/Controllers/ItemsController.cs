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

    }
}
