﻿using Coworking.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Logic.Services;

public class DepartmentService: IDepartmentService
{
    private readonly IApplicationDbContext _db;

    public DepartmentService(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<int> GetDepartmentIdByName(string name)
    {
        var department = await _db.Departments.FirstOrDefaultAsync(dep => dep.Name == name);
        
        if (department == null)
        {
            throw new ArgumentException($"No department with name {name}", nameof(name));
        }
        return department.Id;
    }
}