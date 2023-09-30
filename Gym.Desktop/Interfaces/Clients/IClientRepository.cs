using Gym.Desktop.Entities.Clients;
using System.Threading.Tasks;

namespace Gym.Desktop.Interfaces.Clients;

interface IClientRepository : IRepository<Client, Client>
{
    public Task<int> CountAsync();
}