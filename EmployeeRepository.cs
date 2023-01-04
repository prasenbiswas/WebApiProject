using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using WebApiPrac1.DataContext;

namespace WebApiPrac1.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _Context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _Context = context; 
        }

        public async Task<Employee> AddEmployees(Employee employee)
        {
            var result = await _Context.Employees.AddAsync(employee);
            await _Context.SaveChangesAsync();
            return result.Entity;

        }
        public async Task<Employee> DeleteEmployees(int Id)
        {
            var result = await _Context.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if(result != null)
            {
                _Context.Employees.Remove(result);
            await _Context.SaveChangesAsync();
            return result;
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _Context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployees(int Id)
        {
            var result = await _Context.Employees.FirstOrDefaultAsync(a => a.Id == Id);
            return result;
        }

        public async Task<IEnumerable<Employee>> SearchEmployee(string name)
        {
            IQueryable<Employee> query = _Context.Employees;
            if (! string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name == name);
            }
            return await query.ToListAsync();
        }

        public async Task<Employee> UpdateEmployees(Employee employee)
        {
            var result = await _Context.Employees.FirstOrDefaultAsync(a=>a.Id == employee.Id);
            if(result != null)
            {
                result.Name = employee.Name;
                result.City = employee.City;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
