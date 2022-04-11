﻿using ComputerShop.Shared.Models;

namespace ComputerShop.Client.Services.Categories
{
    public interface ICategoryService
    {
        List<Category> Categories { get; set; }
        Task LoadAsync();
        Task<Category?> GetCategoryById(int id);
        Task<Category?> GetCategoryByUrl(string categoryUrl);
    }
}