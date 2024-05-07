using AutoMapper;
using DLL.IRepository;

public class BaseService<T, TVM> : IBaseService<T, TVM> where T : class where TVM : class
{
    private readonly IBaseRepository<T> _repository;
    private readonly IMapper _mapper;

    public BaseService(IBaseRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TVM> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<TVM>(entity);
    }

    public async Task<IEnumerable<TVM>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TVM>>(entities);
    }

    public async Task<int> AddAsync(TVM viewModel)
    {
        var entity = _mapper.Map<T>(viewModel);
        return await _repository.AddAsync(entity);
    }

    public async Task<int> UpdateAsync(int id, TVM viewModel)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return 0; 

        _mapper.Map(viewModel, entity);
        return await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
