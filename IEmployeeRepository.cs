using DataAccessLayer;

namespace WebApiPrac1.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> SearchEmployee(string name);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployees(int Id);
        Task<Employee> AddEmployees(Employee employee);
        Task<Employee> UpdateEmployees(Employee employee);
        Task<Employee> DeleteEmployees(int Id);
    }
}
