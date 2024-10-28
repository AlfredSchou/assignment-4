using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;



public class DataService : IDataService
{
    
    
    

    public IList<Order> GetOrders()
    {
        var db = new NorthwindContext();
        return db.Orders.ToList();
    }

    public IList<Category> GetCategories()
    {
        var db = new NorthwindContext();
        return db.Categories.ToList();
    }

    public Category GetCategory (int id)
    {
        var db = new NorthwindContext();
        var category = db.Categories.Find(id);
        return category;
      
    }

    public Category CreateCategory(string name, string description)
    {
        var db = new NorthwindContext();

        int id = db.Categories.Max(x => x.Id) + 1;
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        db.Categories.Add(category);

        db.SaveChanges();

        return category;
    }

    public bool DeleteCategory(int id)
    {
        var db = new NorthwindContext();

        var category = db.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        db.Categories.Remove(category);

        return db.SaveChanges() > 0;

    }

    public bool UpdateCategory(int id, string name, string description)
    {
        var db = new NorthwindContext();
        var category = db.Categories.Find(id);

        if (category == null) 
        {
            return false;
        }

        category.Name = name;
        category.Description = description;

        db.SaveChanges();
        return true;
    }

    public ProductDto GetProduct(int id)
    {
        var db = new NorthwindContext();
        var product = db.Products
            .Include(p => p.Category)
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name
            })
            .FirstOrDefault();

        return product;
    }

    public IList <ProductDto> GetProductByCategory(int categoryId)
    {
        var db = new NorthwindContext();
        var products = db.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .Select(p => new ProductDto
            {
                Id  = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name
            })
            .ToList();
            
        return products;
    }

    public IList<ProductDto> GetProductByName(string substring)
    {
        var db = new NorthwindContext();
        var products = db.Products
            .Include(p => p.Category)
            .Where(p => p.Name.Contains(substring))
            .Select(p => new ProductDto
            {
                //Id = p.Id,
                Name=p.Name,
                CategoryName=p.Category.Name
            })
            .ToList();

        return products;
    }

    public Order GetOrder(int orderId)
    {
        var db = new NorthwindContext();
        var order = db.Orders
            .Where(o => o.Id == orderId)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ThenInclude(p => p.Category).FirstOrDefault();

        return order;
    }

    public IList<OrderDetails> GetOrderDetailsByOrderId(int orderId)
    {
        var db = new NorthwindContext();

        var orderDetails = db.OrderDetails
            .Where(od => od.OrderId == orderId)
            .Include(od => od.Product).ToList();
        return orderDetails;
    }

    public IList<OrderDetails> GetOrderDetailsByProductId(int productId)
    {
        var db = new NorthwindContext();
        var orderDetails = db.OrderDetails
            .Where(od => od.ProductId == productId)
            .OrderBy(od => od.OrderId)
            .Select (od => new OrderDetails
            {
                OrderId = od.OrderId,
                UnitPrice = od.UnitPrice,
                Quantity = od.Quantity,
                Order = new Order
                {
                    Date = od.Order.Date,
                }
            }).ToList();

        return orderDetails;
    }

}
