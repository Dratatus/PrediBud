﻿namespace Backend.services
{
    public interface ISupplierService
    {
        Task UpdateSuppliersAsync(string jsonFilePath);
    }
}