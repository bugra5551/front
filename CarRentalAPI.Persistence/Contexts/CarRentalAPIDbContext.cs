using CarRentalAPI.Domain.Entities.Common;
using CarRentalAPI.Domain.Entities.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Persistence.Contexts;

/// <summary>
/// CarRentalAPI veritabanı bağlamını yapılandıran sınıf.
/// </summary>
public class CarRentalAPIDbContext : DbContext
{
    public CarRentalAPIDbContext()
    {
    }

    public CarRentalAPIDbContext(DbContextOptions<CarRentalAPIDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarClass> CarClasses { get; set; }
    public DbSet<SpecificationDescription> SpecificationsDescriptions { get; set; }
    public DbSet<CarSpecification> CarSpecifications { get; set; }
    public DbSet<RentingCondition> RentingConditions { get; set; }
    public DbSet<CarRentingCondition> CarRentingConditions { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<CarImage> CarImages { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<CarDamageDetail> CarDamageDetails { get; set; }
    public DbSet<HelpDesk> HelpDesks { get; set; }


    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Initial Catalog=CarRentalDb; Integrated Security=true;");

        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Employee>().ToTable("Employees");

        //User - UserType ilişkisi
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserType)
            .WithMany(ut => ut.Users)
            .HasForeignKey(u => u.UserTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.CustomerId)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.EmployeeId)
            .IsUnique();

        // Reservation - Customer ilişkisi
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Reservation - Car ilişkisi
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Car)
            .WithMany(v => v.Reservations)
            .HasForeignKey(r => r.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        // Reservation - Payment ilişkisi
        /*modelBuilder.Entity<Reservation>()
             .HasOne(r => r.Payment)
             .WithOne() //p => p.Reservation
             .HasForeignKey<Payment>(p => p.PaymentId)
             .OnDelete(DeleteBehavior.Restrict);
         */

        // Car - Location ilişkisi
        modelBuilder.Entity<Car>()
            .HasOne(v => v.Location)
            .WithMany() //l => l.Cars
            .HasForeignKey(v => v.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        // CarRentingCondition - Car ilişkisi
        modelBuilder.Entity<CarRentingCondition>()
            .HasOne(cc => cc.Car)
            .WithMany(v => v.CarRentingConditions)
            .HasForeignKey(cc => cc.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        // CarRentingCondition - RentingCondition ilişkisi
        modelBuilder.Entity<CarRentingCondition>()
            .HasOne(cc => cc.RentingCondition)
            .WithMany() //rc => rc.CarRentingConditions
            .HasForeignKey(cc => cc.RentingConditionId)
            .OnDelete(DeleteBehavior.Restrict);

        // CarImage - Car ilişkisi

        modelBuilder.Entity<CarImage>()
            .HasOne(cc => cc.Car)
            .WithMany() //vi => vi.CarImages
            .HasForeignKey(cc => cc.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        // Car - CarClass ilişkisi

        modelBuilder.Entity<Car>()
            .HasOne(cc => cc.CarClass)
            .WithMany() //vc => vc.Cars
            .HasForeignKey(cc => cc.CarClassId)
            .OnDelete(DeleteBehavior.Restrict);

        // CarSpecification - Car ilişkisi

        modelBuilder.Entity<CarSpecification>()
            .HasOne(vs => vs.Car)
            .WithMany(v => v.CarSpecifications)
            .HasForeignKey(vs => vs.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        // CarSpecification - SpecificationDescription ilişkisi

        modelBuilder.Entity<CarSpecification>()
            .HasOne(sd => sd.SpecificationDescription)
            .WithMany() //vs => vs.CarSpecifications
            .HasForeignKey(sd => sd.SpecificationDescriptionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Model - Brand ilişkisi
        modelBuilder.Entity<Model>()
            .HasOne(m => m.Brand)
            .WithMany(b => b.Models)
            .HasForeignKey(m => m.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        // CarDamageDetail - Car ilişkisi
        modelBuilder.Entity<CarDamageDetail>()
            .HasOne(d => d.Car)
            .WithMany(c => c.CarDamageDetails)
            .HasForeignKey(d => d.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        // Car - Model ilişkisi
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Model)
            .WithMany(m => m.Cars)
            .HasForeignKey(c => c.ModelId)
            .OnDelete(DeleteBehavior.Restrict);

        // HelpDesk - User ilişkisi
        modelBuilder.Entity<HelpDesk>()
            .HasOne(h => h.User)
            .WithMany()
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}