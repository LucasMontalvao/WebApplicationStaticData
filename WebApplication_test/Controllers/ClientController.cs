using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_test.Models;
using System.Collections;

namespace WebApplication_test.Controllers
{
    public class ClientController : Controller
    {
        public static List<Client> Clients = new List<Client>();
        // GET: ClientController
        public ActionResult Index()
        {
            //adds one client in the list everytime its empty for tests purposes
            if (Clients.Count == 0)
                {
                    Client client = new Client();
                    client.id = 1;
                    client.name = "lucas montalvao";
                    client.cpf = "0000000000";
                    client.genre = "masculino";
                    client.address = "rua aleatoria";
                    Clients.Add(client);
                }
            return View(Clients);
        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            var client = Clients.FirstOrDefault(delegate (Client c) { return c.id == id; });
            return View(client);
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("name,cpf,genre,address")] Client client)
        {
            if (ModelState.IsValid){
                if(Clients.Count == 0)
                {
                    client.id = 1;
                    Clients.Add(client);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Client lastClient = Clients.Last();
                    int newId = lastClient.id + 1;
                    client.id = newId;
                    Clients.Add(client);
                }
                
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = Clients.FirstOrDefault(delegate (Client c) { return c.id == id; });
            return View(client);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("id,name,cpf,genre,address")] Client updatedClient)
        {
            var client = Clients.FirstOrDefault(delegate (Client c) { return c.id == id; });
            Clients.Remove(client);
            client = updatedClient;
            Clients.Insert(client.id - 1, client);
            return RedirectToAction(nameof(Index));
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = Clients.FirstOrDefault(delegate (Client c) { return c.id == id; });
            return View(client);
        }

        // POST: ClientController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            Clients.RemoveAll(delegate (Client c) { return c.id == id; });
            return RedirectToAction(nameof(Index));
        }

    }
}
