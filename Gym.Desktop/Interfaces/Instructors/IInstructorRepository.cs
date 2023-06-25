using Gym.Desktop.Entities.Instructors;
using Gym.Desktop.ViewModels.Instructors;

namespace Gym.Desktop.Interfaces.Instructors;

public interface IInstructorRepository : IRepository<Instructor, InstructorViewModel>
{
}