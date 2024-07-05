using CarRentalAPI.Application.DTOs;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.Services;
using CarRentalAPI.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.API.Controllers;

/// <summary>
/// Müşterilerle ilgili işlemleri yöneten kontrolör.
/// </summary>
[Route("[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IUserService _userService;
    private readonly CarRentalAPIDbContext _context;

    /// <summary>
    /// CustomerController sınıfının kurucusu.
    /// </summary>
    /// <param name="customerService">Müşteri hizmeti.</param>
    /// <param name="userService">Kullanıcı hizmeti.</param>
    /// <param name="context">Veritabanı bağlamı.</param>
    public CustomerController(ICustomerService customerService, IUserService userService, CarRentalAPIDbContext context)
    {
        _customerService = customerService;
        _userService = userService;
        _context = context;
    }

    /// <summary>
    /// Tüm müşterileri getirir.
    /// </summary>
    /// <returns>Müşteri listesi.</returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllCustomer()
    {
        var customers = await _customerService.GetAllAsync();
        return (customers == null || !customers.Any()) ? NotFound("Hiç müşteri bulunamadı.") : Ok(customers);
    }

    /// <summary>
    /// Belirli bir müşteri ID'sine göre müşteri bilgilerini getirir.
    /// </summary>
    /// <param name="customerId">Müşteri ID'si.</param>
    /// <returns>Müşteri bilgileri.</returns>
    [HttpGet("GetByCustomerId/{customerId}")]
    public async Task<IActionResult> GetByIdAsync(int customerId)
    {
        var customer = await _customerService.GetByIdAsync(customerId);
        return customer == null ? NotFound("Müşteri bulunamadı.") : Ok(customer);
    }

    /// <summary>
    /// Yeni bir müşteri kaydı ekler.
    /// </summary>
    /// <param name="userDTO">Kullanıcı bilgileri.</param>
    /// <returns>Kayıt işlemi sonucu.</returns>
    [HttpPost("Register")]
    public async Task<IActionResult> Add(UserDTO userDTO)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var user = new UserDTO
                {
                    IdentityNumber = userDTO.IdentityNumber,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                    PhoneNumber = userDTO.PhoneNumber,
                    DateOfBirth = userDTO.DateOfBirth,
                    Address = userDTO.Address,
                    City = userDTO.City,
                    Country = userDTO.Country,
                    RegistrationDate = DateTime.UtcNow,
                    UserTypeId = 3
                };

                var addedUser = await _userService.Add(user);

                var customer = new CustomerCreateUpdateDTO
                {
                    CustomerId = addedUser.UserId,
                    DriverLicenseNumber = userDTO.DriverLicenseNumber,
                    isDeleted = false
                };

                await _customerService.Add(customer);

                await transaction.CommitAsync();

                return Ok(new { message = "Üyelik başarılı" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new { message = $"Üye olurken bir problem oluştu: {ex.Message}" });
            }
        }
    }

    /// <summary>
    /// Mevcut bir müşteri kaydını günceller.
    /// </summary>
    /// <param name="customerCreateUpdateDTO">Güncellenmek istenen müşteri bilgileri.</param>
    /// <returns>Güncellenen müşteri bilgileri.</returns>
    [HttpPut("Update")]
    public async Task<IActionResult> Update(CustomerCreateUpdateDTO customerCreateUpdateDTO)
    {
        var updatedCustomer = await _customerService.Update(customerCreateUpdateDTO);
        return updatedCustomer == null ? NotFound("Güncellenecek müşteri bulunamadı.") : Ok(updatedCustomer);
    }

    /// <summary>
    /// Belirli bir müşteri ID'sine göre müşteri kaydını siler.
    /// </summary>
    /// <param name="customerId">Silinmek istenen müşteri ID'si.</param>
    /// <returns>Silme işlemi sonucu.</returns>
    [HttpDelete("DeleteByCustomerId/{customerId}")]
    public async Task<IActionResult> Delete(int customerId)
    {
        var isDeleted = await _customerService.Delete(customerId);
        return isDeleted ? Ok("Müşteri kaydı silindi.") : NotFound("Müşteri bulunamadı.");
    }
}
