using Microsoft.AspNetCore.Mvc;
using PlayCommonApp.Repositories;
using PlayInventoryServiceApp.Clients;
using PlayInventoryServiceApp.Helpers;
using PlayInventoryServiceApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayInventoryServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> _repository;
        private readonly CatalogClient _catalogClient;

        public ItemsController(IRepository<InventoryItem> repository, CatalogClient catalogClient)
        {
            _repository = repository;
            _catalogClient = catalogClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrandItemDto>>> GetAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return BadRequest();
                }

                //var items = (await _repository.GetAllSync(item => item.UserId == userId))
                //               .Select(item => item.AsDto());

                var catalogItems = await _catalogClient.GetCatalogAsync();
                var inventoryItemEntities = await _repository.GetAllSync(item => item.UserId == userId);

                var inventoryItemDtos = inventoryItemEntities.Select(inventoryItem =>
                {
                    var catalogItem = catalogItems.FirstOrDefault(x => x.Id == inventoryItem.CatagoryItemId);
                    if (catalogItem != null)
                    {
                        return inventoryItem.AsDto(catalogItem.Name ?? "", catalogItem.Description ?? "");
                    }
                    else
                    {
                        return inventoryItem.AsDto("", "");

                    }

                });

                return Ok(inventoryItemDtos);
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(GrandItemDto grandItemDto)
        {
            var inventoryItem = await _repository.GetSync(x => x.UserId == grandItemDto.UserId
                                                          && x.CatagoryItemId == grandItemDto.CatagoryId);

            if (inventoryItem == null)
            {
                await _repository.CreateSync(grandItemDto.AsModel());
            }
            else
            {
                inventoryItem.Quantity += grandItemDto.Quantity;
                await _repository.UpdateSync(inventoryItem);
            }

            return Ok();
        }

    }
}
