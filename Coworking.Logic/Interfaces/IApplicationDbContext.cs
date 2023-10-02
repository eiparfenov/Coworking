using Coworking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Logic.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<EquipmentModel> EquipmentModels { get; set; }
    public DbSet<Workplace> Workplaces { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Rent> Rents { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}