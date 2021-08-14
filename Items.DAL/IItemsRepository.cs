using System.Collections.Generic;
using System.Threading.Tasks;
using Items.Models;

namespace Items.DAL
{
    public interface IItemsRepository : IRepository<Item>
    {
        Task<IEnumerable<Item>> GetAll();
    }
}
