﻿using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services
{
    public interface IProductsService
    {
        List<Product> Products { get; set; }
        Task LoadAsync();
        Task LoadAsync(int id);
        Task<Product?> GetProductById(int id);
        public Task<T?> GetProductGeneric<T>(int id) where T : Product;
    }
}