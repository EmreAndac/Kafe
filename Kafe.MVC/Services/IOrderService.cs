using Kafe.Entities.Entity.Concrete;

namespace Kafe.MVC.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrdersByTableId(int tableId);
        void AddOrder(Order order);
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id); // Bu metodun tanımlı olduğundan emin olun
    }
}

