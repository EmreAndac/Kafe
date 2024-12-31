using Kafe.DAL.Repositories.Abstract;
using Kafe.Entities.Entity.Concrete;

namespace Kafe.MVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> GetOrdersByTableId(int tableId)
        {
            return _orderRepository.GetAll(o => o.TableId == tableId);
        }

        public void AddOrder(Order order)
        {
            _orderRepository.Create(order);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetById(id);
        }
    }
}
