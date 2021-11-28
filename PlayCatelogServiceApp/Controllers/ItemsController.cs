using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayCatelogServiceApp.Helpers;
using PlayCatelogServiceApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayCatelogServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly ItemRepository _itemRepository = new();

        private static readonly List<ItemDto> items = new List<ItemDto>()
        {
            new ItemDto(Guid.NewGuid(),"Pen","Red Color Pen",10,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Book","Sample books",300,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Showes","small showes",560,DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Keyboard","Mini Keyboard",550,DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> Get()
        {
            var itemsToReturn = (await _itemRepository.GetAllSync()).Select(x => x.AsDto());
            return itemsToReturn;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemById(Guid id)
        {
            var item = await _itemRepository.GetSync(id);
            // var item = items.Where(x => x.Id == id).SingleOrDefault();

            if (item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }


        [HttpPost]
        public async Task<ActionResult<ItemDto>> Post(CreateItemDto createItemDto)
        {
            //var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            //items.Add(item);
            var item = createItemDto.AsCreateModel();
            await _itemRepository.CreateSync(item);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDto>> Put(Guid id, UpdateItemDto updateItemDto)
        {
            //var item = items.Where(x => x.Id == id).SingleOrDefault();

            //if (item == null)
            //{
            //    return NotFound();
            //}

            //var updateItem = item with
            //{
            //    Name = updateItemDto.Name,
            //    Description = updateItemDto.Description,
            //    Price = updateItemDto.Price,
            //};

            //var index = items.FindIndex(x => x.Id == id);
            //items[index] = updateItem;

            var item = updateItemDto.AsUpdateModel(id);
            var itemFromDB = await _itemRepository.GetSync(id);
            if (itemFromDB == null)
            {
                return NotFound();
            }

            await _itemRepository.UpdateSync(item);

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var index = items.FindIndex(x => x.Id == id);

            //if (index < 0)
            //{
            //    return NotFound();
            //}
            //items.RemoveAt(index);

            var itemFromDB = await _itemRepository.GetSync(id);
            if (itemFromDB == null)
            {
                return NotFound();
            }

            await _itemRepository.RemoveSync(itemFromDB);

            return NoContent();
        }


    }
}
