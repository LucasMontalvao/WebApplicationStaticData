using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication_test.Models;

namespace WebApplication_test.Controllers
{
    public class ProductOrderController : Controller
    {
        public static List<ProductOrder> ProductOrders = new List<ProductOrder>();
        
        // GET: ProductOrderController
        public ActionResult Index()
        {
            return View(ProductOrders);
        }

        // GET: ProductOrderController/Details/5
        public ActionResult Details(int id)
        {
            var productOrder = ProductOrders.FirstOrDefault(delegate (ProductOrder p) { return p.orderId == id; });
            ViewBag.orderId = id;
            return View(productOrder);
        }

        // GET: ProductOrderController/Create
        public ActionResult Create()
        {
            ProductOrder productOrder = new ProductOrder();
            if (ProductOrders.Count == 0)
            {
                productOrder.orderId = 1;
                ProductOrders.Add(productOrder);
            }
            else
            {
                if (productOrder.orderId == 0)
                {
                    ProductOrder lastProductOrder = ProductOrders.Last();
                    productOrder.orderId = lastProductOrder.orderId + 1;
                    ProductOrders.Add(productOrder);
                }
            }
            ViewBag.clientId = new SelectList
                (
                    ClientController.Clients,
                    "id",
                    "name"
                );
            return View(productOrder);
        }

        // POST: ProductOrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("orderId,clientId,totalOrderValue,totalOrderValueDescount")] ProductOrder productOrder)
        {
            if(productOrder.clientId != 0)
            {
                productOrder.getClient(productOrder.clientId);
                var clientProductOrder = ProductOrders.FirstOrDefault(delegate (ProductOrder p) { return p.orderId == productOrder.orderId; });
                if(clientProductOrder != null) 
                { 
                    clientProductOrder.Client = productOrder.Client;
                    clientProductOrder.clientId = productOrder.clientId;  
                }
            }
            return Json(productOrder.orderId);
        }

        // GET: ProductOrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductOrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var productOrder = ProductOrders.FirstOrDefault(delegate (ProductOrder p) { return p.orderId == id; });
            ViewBag.orderId = id;
            return View(productOrder);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            ProductOrders.RemoveAll(delegate (ProductOrder p) { return p.orderId == id; });
            OrderItensController.orderItens.RemoveAll(delegate (OrderItens o) { return o.productOrder.orderId == id; });
            return RedirectToAction("Index");
        }
    }
}
