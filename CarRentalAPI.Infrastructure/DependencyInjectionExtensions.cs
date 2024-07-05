using CarRentalAPI.Application.Services;
using CarRentalAPI.Application.Services.ServiceImpl;
using CarRentalAPI.Persistence.Repositories;
using CarRentalAPI.Persistence.Repositories.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalAPI.Infrastructure;

/// <summary>
/// Proje bağımlılıklarını yapılandıran genişletme metodları.
/// </summary>
public static class DependencyInjectionExtensions
{
    public static void AddProjectDependencies(this IServiceCollection services)
    {
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBrandService, BrandServiceImpl>();
        services.AddScoped<IModelRepository, ModelRepository>();
        services.AddScoped<IModelService, ModelServiceImpl>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICarService, CarServiceImpl>();
        services.AddScoped<ICarClassRepository, CarClassRepository>();
        services.AddScoped<ICarClassService, CarClassServiceImpl>();
        services.AddScoped<ICarDamageDetailRepository, CarDamageDetailRepository>();
        services.AddScoped<ICarDamageDetailService, CarDamageDetailServiceImpl>();
        services.AddScoped<ICarImageRepository, CarImageRepository>();
        services.AddScoped<ICarImageService, CarImageServiceImple>();
        services.AddScoped<ICarRentingConditionRepository, CarRentingConditionRepository>();
        services.AddScoped<ICarRentingConditionService, CarRentingConditionServiceImpl>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerServiceImpl>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeServiceImpl>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentService, PaymentServiceImpl>();
        services.AddScoped<IRentingConditionRepository,  RentingConditionRepository>();
        services.AddScoped<IRentingConditionService , RentingConditionServiceImpl>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ILocationService, LocationServiceImpl>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IReservationService, ReservationServiceImpl>();
        services.AddScoped<ISpecificationDescriptionRepository, SpecificationDescriptionRepository>();
        services.AddScoped<ISpecificationDescriptionService, SpecificationDescriptionServiceImpl>();
        services.AddScoped<ICarSpecificationRepository, CarSpecificationRepository>();
        services.AddScoped<ICarSpecificationService, CarSpecificationServiceImpl>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserServiceImpl>();
        services.AddScoped<IHelpDeskRepository, HelpDeskRepository>();
        services.AddScoped<IHelpDeskService, HelpDeskServiceImpl>();
    }
}