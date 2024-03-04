using Alasson.Interfaces;
using Alasson.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Alasson.Service
{
    public class EmployeesService : BaseService, IEmployeesService
    {

        public EmployeesService(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task AddAsync(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
                await CompleteAsync();
            }
            catch (Exception ex)
            { }
        }
        public async Task<Employee> UpdateAsync(string fullname, int key, string value)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(fullname);
                if (employee != null)
                {
                    switch (key) {
                        case 1:
                            employee.Email = value;

                            break;
                        case 2:
                            employee.Charge = value;
                            break;
                        case 3:
                            if (float.TryParse(value, out float salary)) employee.Salary = salary;
                            break;
                        default:
                            break;
                    }
                    await CompleteAsync();
                    return employee;

                }
                else
                {
                    return null;
                }

            } catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<string> DeleteAsync(string fullname)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(fullname);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await CompleteAsync();
                    return "Employee Deleted Succesfully";
                }
                return "Employee Not Found";
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
  
}
