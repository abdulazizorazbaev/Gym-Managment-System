using Gym.Desktop.Entities.Memberships;
using System.Threading.Tasks;

namespace Gym.Desktop.Interfaces.Memberships;

public interface IMembershipRepository : IRepository<Membership, Membership>
{
    public Task<int> CountAsync();
}
