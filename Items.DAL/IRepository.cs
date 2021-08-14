using System.Threading.Tasks;
using Items.Models;

namespace Items.DAL
{
    public interface IRepository<T>
    {
        Task Create(T obj);

        Task<Item> Get(int id);

        Task Update(T obj);

        Task Delete(int id);
    }
}
