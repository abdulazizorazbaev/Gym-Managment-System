using Gym.Desktop.Entities.Sessions;
using Gym.Desktop.Interfaces.Sessions;
using Gym.Desktop.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Desktop.Repositories.Sessions;

public class SessionRepository : ISessionRepository
{
    public Task<int> CountAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<int> CreateAsync(Session obj)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> DeleteAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<IList<Session>> GetAllAsync(PaginationParams @params)
    {
        throw new System.NotImplementedException();
    }

    public Task<Session> GetAsync(long id)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Session editedObj)
    {
        throw new System.NotImplementedException();
    }
}