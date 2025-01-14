﻿namespace WebApi.Models;

public class ProductModel
{
    public string? Url { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
}
