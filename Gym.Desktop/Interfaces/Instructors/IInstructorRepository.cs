using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.ViewModels.Instructors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Desktop.Interfaces.Instructors;

public interface IInstructorRepository : IRepository<Instructor, Instructor>
{
    public Task<IList<Instructor>> GetAllByPackageIdAsync (long packageId);
}