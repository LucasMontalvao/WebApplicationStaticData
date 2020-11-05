using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_test.Models;

namespace WebApplication_test.Controllers
{
    public class ProductController : Controller
    {
        public static List<Product> Products = new List<Product>();
        // GET: ProductController
        public ActionResult Index()
        {
            if(Products.Count == 0)
            {
                Product product = new Product();
                product.id = 1;
                product.description = "Batata salsa";
                product.price = 100.00m;
                product.quantity = 10;
                product.discount = 10;
                Products.Add(product);
            }
            return View(Products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = Products.FirstOrDefault(delegate (Product p) { return p.id == id; });
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("description,quantity,price,discount")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (Products.Count == 0)
                {
                    product.id = 1;
                    Products.Add(product);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Product lastProduct = Products.Last();
                    int newId = lastProduct.id + 1;
                    lastProduct.id = newId;
                    Products.Add(product);
                }

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = Products.FirstOrDefault(delegate (Product p) { return p.id == id; });
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("id,description,quantity,price,discount")] Product updatedProduct)
        {
            var product = Products.FirstOrDefault(delegate (Product p) { return p.id == id; });
            Products.Remove(product);
            product = updatedProduct;
            Products.Insert(product.id - 1, product);
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = Products.FirstOrDefault(delegate (Product p) { return p.id == id; });
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            Products.RemoveAll(delegate (Product p) { return p.id == id; });
            return RedirectToAction("Index");
        }

        public List<Product> returnAllProducts()
        {
            return Products;
        }
    }
}
