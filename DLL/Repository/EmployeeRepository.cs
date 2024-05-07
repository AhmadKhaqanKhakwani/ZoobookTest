using DLL.IRepository;
using Model.Entities;
using System.Data;

namespace DLL.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(IDbConnection dbConnection) : base(null)
        {
            _dbConnection = dbConnection;
        }
    }
}
