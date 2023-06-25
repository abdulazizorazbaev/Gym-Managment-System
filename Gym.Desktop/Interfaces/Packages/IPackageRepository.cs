using Gym.Desktop.Entities.Packages;
using System.Threading.Tasks;

namespace Gym.Desktop.Interfaces.Packages;

public interface IPackageRepository : IRepository<Package, Package>
{
    public Task<int> CountAsync();
}
