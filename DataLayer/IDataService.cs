using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IDataService
    {
        IList<Order> GetOrders();
        IList<Category> GetCategories();
        Category GetCategory(int id);
        Category CreateCategory(string name, string description);
        bool DeleteCategory(int id);
        bool UpdateCategory(int id, string name, string description);
        ProductDto GetProduct(int id);
        IList<ProductDto> GetProductByCategory(int id);
        IList<ProductDto> GetProductByName(string name);
        Order GetOrder(int orderId);
        IList<OrderDetails> GetOrderDetailsByOrderId(int orderId);
        IList<OrderDetails> GetOrderDetailsByProductId(int productId);


    }
}
