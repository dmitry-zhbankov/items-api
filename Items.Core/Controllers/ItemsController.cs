using System.Collections.Generic;
using System.Threading.Tasks;
using Items.DAL;
using Items.Models;
using Microsoft.AspNetCore.Mvc;

namespace Items.Core.Controllers
{
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:int}")]
        public async Task<Item> GetItem(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _repository.GetAll();
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteItem(int id)
        {
            await _repository.Delete(id);
        }

        [HttpPost]
        public async Task CreateItem([FromBody] Item item)
        {
            await _repository.Create(item);
        }

        [HttpPatch]
        public async Task UpdateItem([FromBody] Item item)
        {
            await _repository.Update(item);
        }
    }
}
