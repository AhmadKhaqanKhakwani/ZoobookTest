using AutoMapper;
using BLL.IServices;
using DLL.IRepository;
using Model.Entities;
using Model.ViewModels;
namespace BLL.Services
{
    public class EmployeeService : BaseService<Employee, EmployeeVM>, IEmployeeService
    {
        public EmployeeService(IBaseRepository<Employee> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}