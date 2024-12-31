using Kafe.BL.Manager.Abstract;
using Kafe.Entities.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Kafe.MVC.Controllers
{
	public class TableController : Controller
	{
		private readonly IManager<Table> _tableManager;
		private readonly IManager<Category> categoryManager;
		private readonly IManager<Product> productManager;
		private readonly IManager<OrderItem> orderItemManager;
		private readonly IManager<Order> orderManager;

		public TableController(IManager<Table> tableManager, IManager<Category> categoryManager, IManager<Product> productManager, 
			IManager<OrderItem> orderItemManager)
		{
			this.orderItemManager = orderItemManager;
			this._tableManager = tableManager;
			this.categoryManager = categoryManager;
			this.productManager = productManager;
		}

		// Masaların listesi
		public IActionResult Index()
		{
			var tables = _tableManager.GetAll();
			if (tables == null || !tables.Any())
			{
				return View("Error", "Masalar bulunamadı.");
			}
			return View(tables);
		}

        // Masa ayrıntıları ve ürünlerin listesi
        public IActionResult Details(int id)
        {
            var table = _tableManager.GetById(id);
            if (table == null)
            {
                return NotFound();
            }
            var orders = orderManager?.GetAll(); // orderManager'ın null olabileceğini kontrol edin
            if (orders == null)
            {
                // Loglama veya hata mesajı ekleyin
                return View("Error", "Siparişler yüklenemedi.");
            }


            var torders = orderManager.GetAll()
                .Where(o => o.TableId == id)
                .Select(o => new
                {
                    o.Id,
                    o.CreatedDate,
                    o.IsPayed,
                    Items = o.OrderItems.Select(oi => new
                    {
                        oi.Product.Name,
                        oi.Quantity,
                        oi.Product.Price
                    })
                }).ToList();

            var categories = categoryManager.GetAll();
            var model = new Tuple<Table, IEnumerable<Category>, IEnumerable<object>>(table, categories, orders);

            return View(model);
        }


        // Kategoriye göre ürünleri getirme
        [HttpGet]
		public IActionResult GetProductsByCategory(int categoryId)
		{
			var products = productManager.GetAll().Where(p => p.CategoryId == categoryId);
			return Json(products);
		}

		// Sipariş ekleme işlemi
		[HttpPost]
		public IActionResult AddOrderItem(int tableId, int productId, int quantity)
		{
			var orderItems = TempData["OrderItems"] != null
				? JsonConvert.DeserializeObject<List<OrderItem>>(TempData["OrderItems"].ToString())
				: new List<OrderItem>();

			var product = productManager.GetById(productId);
			if (product != null)
			{
				var existingItem = orderItems.FirstOrDefault(i => i.ProductId == productId);
				if (existingItem != null)
				{
					existingItem.Quantity += quantity;
				}
				else
				{
					orderItems.Add(new OrderItem { ProductId = productId, Product = product, Quantity = quantity });
				}

				TempData["OrderItems"] = JsonConvert.SerializeObject(orderItems);
			}

			return Json(orderItems);
		}

		// Siparişten ürün çıkarma işlemi
		[HttpPost]
		public IActionResult RemoveOrderItem(int tableId, int productId)
		{
			var orderItems = TempData["OrderItems"] != null
				? JsonConvert.DeserializeObject<List<OrderItem>>(TempData["OrderItems"].ToString())
				: new List<OrderItem>();

			orderItems = orderItems.Where(i => i.ProductId != productId).ToList();

			TempData["OrderItems"] = JsonConvert.SerializeObject(orderItems);
			return Json(orderItems);
		}

        // Siparişleri kaydetme işlemi
        [HttpPost]
        [HttpPost]
        public IActionResult SaveOrder(int tableId, List<OrderItem> orderItems)
        {
            var table = _tableManager.GetById(tableId);

            if (table == null)
            {
                return NotFound("Masa bulunamadı.");
            }

            // Yeni sipariş oluştur
            var order = new Order
            {
                TableId = tableId,
                CreatedDate = DateTime.Now,
                IsPayed = false,
                OrderItems = orderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Product = productManager.GetById(item.ProductId)
                }).ToList()
            };

            orderManager.Add(order);

            // Masayı meşgul olarak işaretle
            table.IsOccupied = true;
            _tableManager.Update(table);

            return Json(new { success = true, message = "Sipariş kaydedildi." });
        }
        [HttpPost]
        public IActionResult PayOrder(int orderId)
        {
            var order = orderManager.GetById(orderId);
            if (order == null)
            {
                return NotFound("Sipariş bulunamadı.");
            }

            order.IsPayed = true;
            order.UpdatedDate = DateTime.Now;
            orderManager.Update(order);

            // Eğer masadaki tüm siparişler ödendiyse masayı boş olarak işaretle
            var table = _tableManager.GetById(order.TableId);
            if (table != null && !table.Orders.Any(o => !o.IsPayed))
            {
                table.IsOccupied = false;
                _tableManager.Update(table);
            }

            return Json(new { success = true, message = "Ödeme işlemi tamamlandı ve masa boş olarak işaretlendi." });
        }


        // Masaya ait önceki siparişleri getir
        public IActionResult GetTableOrders(int tableId)
        {
            var orders = orderManager.GetAll()
                .Where(o => o.TableId == tableId && !o.IsPayed)
                .Select(o => new
                {
                    o.Id,
                    o.CreatedDate,
                    Items = o.OrderItems.Select(oi => new
                    {
                        oi.Product.Name,
                        oi.Quantity,
                        oi.Product.Price
                    }),
                    TotalPrice = o.OrderItems.Sum(oi => oi.Product.Price * oi.Quantity)
                }).ToList();

            return Json(orders);
        }



    }
}