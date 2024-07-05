using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Repositories.Repository;

/// <summary>
/// Çalışan CRUD operasyonları için depo sınıfı.
/// </summary>
public class EmployeeRepository : IEmployeeRepository
{
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// EmployeeRepository sınıfının kurucusu.
    /// </summary>
    /// <param name="context">Veritabanı bağlamı.</param>
    public EmployeeRepository(CarRentalAPIDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir çalışan ekler.
    /// </summary>
    /// <param name="employee">Eklenecek çalışan.</param>
    /// <returns>Eklenen çalışan.</returns>
    public async Task<Employee> Add(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    /// <summary>
    /// Bir çalışanı siler (işaretler).
    /// </summary>
    /// <param name="employee">Silinecek çalışan.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(Employee employee)
    {
        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;

    }

    /// <summary>
    /// Tüm çalışanları getirir.
    /// </summary>
    /// <returns>Tüm çalışanların listesi.</returns>
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    /// <summary>
    /// Belirli bir çalışanı ID ile getirir.
    /// </summary>
    /// <param name="employeeId">Getirilecek çalışanının ID'si.</param>
    /// <returns>Belirli çalışanın bilgileri.</returns>
    public async Task<Employee> GetByIdAsync(int employeeId)
    {
        return await _context.Employees
                             .FirstOrDefaultAsync(b => b.EmployeeId == employeeId);
    }

    /// <summary>
    /// Bir çalışanı günceller.
    /// </summary>
    /// <param name="employee">Güncellenecek çalışan.</param>
    /// <returns>Güncellenen çalışan.</returns>
    public async Task<Employee> Update(Employee employee)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(b => b.EmployeeId == employee.EmployeeId);
        if (existingEmployee != null)
        {
            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _context.SaveChangesAsync();
            return existingEmployee;
        }
        return null;
    }
}
