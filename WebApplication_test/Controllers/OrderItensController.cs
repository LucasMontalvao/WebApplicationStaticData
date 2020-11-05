using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApplication_test.Models;

namespace WebApplication_test.Controllers
{
    public class OrderItensController : Controller
    {
        public static List<OrderItens> orderItens = new List<OrderItens>();
        public ActionResult AddProducts(int orderId)
        {
            decimal totalValue = 0.00m;
            decimal totalValueWithDiscount = 0.00m;
            ViewBag.productId = new SelectList
                (
                    ProductController.Products,
                    "id",
                    "productDescription"
                );
            ViewBag.OrderItens = orderId;
            List<OrderItens> currentOrderIten = orderItens.
                Where(o => o.productOrder.orderId == orderId).ToList();
            foreach(OrderItens item in currentOrderIten)
            {
                totalValue += item.product.price * item.Quantity;
                totalValueWithDiscount += (item.product.price * item.Quantity) - ((item.product.price * item.Quantity * item.unitPriceDescount) / 100);
            }
            ViewBag.totalOrderValue = totalValue;
            ViewBag.totalValueWithDiscount = totalValueWithDiscount;
            updateProductOrder(orderId, totalValue, totalValueWithDiscount);
            return PartialView(currentOrderIten);
        }
        public ActionResult saveItens(int orderId, int quantity, int productId, decimal clientDiscount)
        {
            var currentProduct = ProductController.Products.
                    FirstOrDefault(delegate (Product p) { return p.id == productId; });
            if (orderItens.Count == 0)
            {
                OrderItens orderIten = new OrderItens();
                orderIten.id = 1;
                orderIten.productOrder = ProductOrderController.ProductOrders.
                    FirstOrDefault(delegate (ProductOrder p) { return p.orderId == orderId; });
                orderIten.idProduct = currentProduct.id;
                orderIten.Quantity = quantity;
                orderIten.product = currentProduct;
                orderIten.unitPriceDescount = ((orderIten.product.price * clientDiscount) / 100);
                orderItens.Add(orderIten);
                updateStock(productId, quantity);
            }
            else
            {
                OrderItens orderIten = new OrderItens();
                orderIten.id = orderItens.Last().id + 1;
                orderIten.productOrder = ProductOrderController.ProductOrders.
                    FirstOrDefault(delegate (ProductOrder p) { return p.orderId == orderId; });
                orderIten.idProduct = currentProduct.id;
                orderIten.Quantity = quantity;
                orderIten.product = currentProduct;
                orderIten.unitPriceDescount = ((orderIten.product.price * clientDiscount) / 100);
                orderItens.Add(orderIten);
                updateStock(productId, quantity);
            }
            return Json(orderItens.Last().id);
        }
        public Boolean CheckQuantity(int productId, int quantity)
        {
            Product currentProduct = ProductController.Products.
                FirstOrDefault(delegate (Product p) { return p.id == productId; });
            if (currentProduct != null)
            {
                if (quantity <= currentProduct.quantity)
                {
                    return false;
                }
            }
            return true;
        }
        public ActionResult CheckDiscount(int productId, decimal clientDiscount)
        {
            Product currentProduct = ProductController.Products.
                FirstOrDefault(delegate (Product p) { return p.id == productId; });
            
            if(currentProduct != null)
            {
                if (clientDiscount <= currentProduct.discount)
                {
                    var response = new
                    {
                        maxDiscount = currentProduct.discount,
                        result = true
                    };
                    return Json(response);
                }
                else
                {
                    var response = new
                    {
                        maxDiscount = currentProduct.discount,
                        result = false
                    };
                    return Json(response);
                }
            }
            else
            {
                var response = new
                {
                    maxDiscount = "notfound",
                    result = false
                };
                return Json(response);
            }
        }
        public void updateStock(int productId, int quantity)
        {
            Product currentProduct = ProductController.Products.
                FirstOrDefault(delegate (Product p) { return p.id == productId; });
            currentProduct.quantity -= quantity;
        }
        public void updateProductOrder(int orderId, decimal totalValue, decimal totalValueWithDiscount)
        {
            ProductOrder currenteProductOrder = ProductOrderController.ProductOrders.
                FirstOrDefault(delegate (ProductOrder p) { return p.orderId == orderId; });
            currenteProductOrder.totalOrderValue = totalValue;
            currenteProductOrder.totalOrderValueDescount = totalValueWithDiscount;
        }

        public ActionResult GetItens(int orderId)
        {
            List<OrderItens> currentOrderIten = orderItens.
                Where(o => o.productOrder.orderId == orderId).ToList();
            return PartialView(currentOrderIten);
        }
    }
}
