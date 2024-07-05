using AutoMapper;
using CarRentalAPI.Application.DTOs;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Common;
using CarRentalAPI.Domain.Entities.Entity;

namespace CarRentalAPI.Application;

/// <summary>
/// AutoMapper profilini yapılandıran sınıf, nesneler arası dönüşümleri (mapping) kolaylaştırır.
/// </summary>
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Brand, BrandDTO>().ReverseMap();
        CreateMap<Brand, BrandCreateUpdateDTO>().ReverseMap();

        CreateMap<CarClass, CarClassDTO>().ReverseMap();
        CreateMap<CarClass, CarClassCreateUpdateDTO>().ReverseMap();

        CreateMap<CarDamageDetail, CarDamageDetailDTO>().ReverseMap();
        CreateMap<CarDamageDetail, CarDamageDetailCreateUpdateDTO>().ReverseMap();

        CreateMap<Car, CarDTO>().ReverseMap();
        CreateMap<Car, CarCreateUpdateDTO>().ReverseMap();

        CreateMap<CarImage, CarImageDTO>().ReverseMap();
        CreateMap<CarImage, CarImageCreateUpdateDTO>().ReverseMap();

        CreateMap<CarRentingCondition, CarRentingConditionDTO>().ReverseMap();
        CreateMap<CarRentingCondition, CarRentingConditionCreateUpdateDTO>().ReverseMap();

        CreateMap<CarSpecification, CarSpecificationDTO>().ReverseMap();
        CreateMap<CarSpecification, CarSpecificationCreateUpdateDTO>().ReverseMap();

        CreateMap<Customer, CustomerDTO>().ReverseMap();
        CreateMap<Customer, CustomerCreateUpdateDTO>().ReverseMap();

        CreateMap<Employee, EmployeeDTO>().ReverseMap();
        CreateMap<Employee, EmployeeCreateUpdateDTO>().ReverseMap();

        CreateMap<Location, LocationDTO>().ReverseMap();
        CreateMap<Location, LocationCreateUpdateDTO>().ReverseMap();

        CreateMap<Model, ModelDTO>().ReverseMap();
        CreateMap<Model, ModelCreateUpdateDTO>().ReverseMap();

        CreateMap<Payment, PaymentDTO>().ReverseMap();
        CreateMap<Payment, PaymentCreateUpdateDTO>().ReverseMap();

        CreateMap<RentingCondition, RentingConditionDTO>().ReverseMap();
        CreateMap<RentingCondition, RentingConditionCreateUpdateDTO>().ReverseMap();

        CreateMap<Reservation, ReservationDTO>().ReverseMap();
        CreateMap<Reservation, ReservationCreateUpdateDTO>().ReverseMap();

        CreateMap<SpecificationDescription, SpecificationDescriptionDTO>().ReverseMap();
        CreateMap<SpecificationDescription, SpecificationDescriptionCreateUpdateDTO>().ReverseMap();

        CreateMap<User, UserDTO>().ReverseMap();

        CreateMap<UserType, UserTypeDTO>().ReverseMap();
        CreateMap<UserType, UserTypeCreateUpdateDTO>().ReverseMap();

        CreateMap<HelpDesk, HelpDeskDTO>().ReverseMap();
        CreateMap<HelpDesk, HelpDeskCreateUpdateDTO>().ReverseMap();
    }
}
