﻿using Coworking.Domain.Models;

namespace Coworking.Logic.Interfaces;

public interface IEquipmentsService
{
    Task<List<Equipment>> ReadAllEquipments(int departmentId, int equipmentModelId);
    Task<Equipment> ReadEquipment(int equipmentId, int departmentId);
    Task UpdateEquipment(int equipmentId, int departmentId, string invNumber, string comment);
    Task DeleteEquipment(int equipmentModelId, int departmentId);
    Task<int> CreateEquipment(string invNumber, string comment, int equipmentModelId, int departmentId);
}