using Gym.Desktop.Entities.Sessions;
using System.Threading.Tasks;

namespace Gym.Desktop.Interfaces.Sessions;

public interface ISessionRepository : IRepository<Session, Session>
{
    public Task<int> CountAsync();
}