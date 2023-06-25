using Gym.Desktop.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Desktop.Interfaces;

public interface IRepository<TModel, TViewModel>
{
    public Task<int> CreateAsync(TModel obj);

    public Task<int> UpdateAsync(long id, TModel editedObj);

    public Task<int> DeleteAsync(long id);

    public Task<IList<TViewModel>> GetAllAsync(PaginationParams @params);

    public Task<TViewModel> GetAsync(long id);
}