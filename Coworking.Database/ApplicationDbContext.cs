using Coworking.Domain.Models;
using Coworking.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Test;

public class ApplicationDbContext: DbContext, IApplicationDbContext
{
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Equipment> Equipments { get; set; } = null!;
    public DbSet<EquipmentModel> EquipmentModels { get; set; } = null!;
    public DbSet<Workplace> Workplaces { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;
    public DbSet<Rent> Rents { get; set; } = null!;

    protected ApplicationDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
        FillDb();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("test");
    }
/*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var department = new Department() { Name = "rtu-tc", WorkDayStart = new TimeOnly(10, 0), WorkDayEnd = new TimeOnly(18,0)};
        var users = new List<User>()
        {
            new () { Name = "Саша" },
            new () { Name = "Вася" },
            new () { Name = "Коля" },
        };      
        var equipmentModels = new List<EquipmentModel>()
        {
            new () { Name = "Vive", Department = department},
            new () { Name = "Computer", Department = department},
            new () { Name = "Laptop", Department = department},
        };
        var equipments = new List<Equipment>()
        {
            new() { InvNumber = "Vive1", Model = equipmentModels[0] },
            new() { InvNumber = "Vive2", Model = equipmentModels[0] },
            new() { InvNumber = "Computer1", Model = equipmentModels[1] },
            new() { InvNumber = "Computer2", Model = equipmentModels[1] },
            new() { InvNumber = "Laptop1", Model = equipmentModels[2] },
            new() { InvNumber = "Laptop2", Model = equipmentModels[2] },
            new() { InvNumber = "Laptop3", Model = equipmentModels[2] },
        };
        var workplaces = new List<Workplace>()
        {
            new()
            {
                Name = "Vr station", 
                Department = department, 
                EquipmentModels = new List<EquipmentModel>()
                {
                    equipmentModels[0],
                    equipmentModels[1],
                    equipmentModels[2],
                },
            },
            new()
            {
                Name = "with computer", 
                Department = department, 
                EquipmentModels = new List<EquipmentModel>()
                {
                    equipmentModels[1],
                    equipmentModels[2],
                },
            },
            new()
            {
                Name = "empty", 
                Department = department, 
                EquipmentModels = new List<EquipmentModel>()
                {
                    equipmentModels[2],
                },
            },
        };

        modelBuilder.Entity<Department>().HasData(department);
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<EquipmentModel>().HasData(equipmentModels);
        modelBuilder.Entity<Equipment>().HasData(equipments);
        modelBuilder.Entity<Workplace>().HasData(workplaces);
    }*/

    public void FillDb()
    {
        var department = new Department() { Name = "rtu-tc", WorkDayStart = new TimeOnly(10, 0), WorkDayEnd = new TimeOnly(18,0)};
        var users = new List<User>()
        {
            new () { Name = "Саша" },
            new () { Name = "Вася" },
            new () { Name = "Коля" },
        };      
        var equipmentModels = new List<EquipmentModel>()
        {
            new () { Name = "Vive", Department = department},
            new () { Name = "Computer", Department = department},
            new () { Name = "Laptop", Department = department},
        };
        var equipments = new List<Equipment>()
        {
            new() { InvNumber = "Vive1", Model = equipmentModels[0] },
            new() { InvNumber = "Vive2", Model = equipmentModels[0] },
            new() { InvNumber = "Computer1", Model = equipmentModels[1] },
            new() { InvNumber = "Computer2", Model = equipmentModels[1] },
            new() { InvNumber = "Laptop1", Model = equipmentModels[2] },
            new() { InvNumber = "Laptop2", Model = equipmentModels[2] },
            new() { InvNumber = "Laptop3", Model = equipmentModels[2] },
        };
        var workplaces = new List<Workplace>()
        {
            new()
            {
                Name = "Vr station", 
                Department = department, 
                EquipmentModels = new List<EquipmentModel>()
                {
                    equipmentModels[0],
                    equipmentModels[1],
                    equipmentModels[2],
                },
            },
            new()
            {
                Name = "with computer", 
                Department = department, 
                EquipmentModels = new List<EquipmentModel>()
                {
                    equipmentModels[1],
                    equipmentModels[2],
                },
            },
            new()
            {
                Name = "empty", 
                Department = department, 
                EquipmentModels = new List<EquipmentModel>()
                {
                    equipmentModels[2],
                },
            },
        };

        Departments.Add(department);
        SaveChanges();
        
        Users.AddRange(users);
        SaveChanges();

        equipmentModels.AddRange(equipmentModels);
        SaveChanges();
        
        Workplaces.AddRange(workplaces);
        SaveChanges();
        
        Equipments.AddRange(equipments);
        SaveChanges();
    }
}