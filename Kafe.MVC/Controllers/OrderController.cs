using Kafe.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kafe.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService; // Sipariş servisinden veri çekmek için.

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Sipariş sayfası - Sipariş ekleme işlemleri yapılabilir
        public IActionResult Create()
        {
            return View();
        }

        // Siparişleri listeleme
        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders(); // Servisten tüm siparişleri alır
            return View(orders);
        }

        // Siparişin detaylarını göster
        public IActionResult Details(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }


}

