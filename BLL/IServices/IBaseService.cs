public interface IBaseService<T, TVM> where T : class where TVM : class
{
    Task<TVM> GetByIdAsync(int id);
    Task<IEnumerable<TVM>> GetAllAsync();
    Task<int> AddAsync(TVM viewModel);
    Task<int> UpdateAsync(int id, TVM viewModel);
    Task DeleteAsync(int id);
}
