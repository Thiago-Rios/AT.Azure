using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace AT.WebMVC.Controllers
{
    public class AmigoController : Controller
    {
        public ActionResult Index()
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/amigos", DataFormat.Json);
            var response = restClient.Get<List<Amigo>>(request);

            return View(response.Data);
        }

        // GET: Amigo/Create
        public ActionResult Create()
        {
            var restClient = new RestClient();
            var request = new RestRequest("http://localhost:5000/api/pessoas", DataFormat.Json);
            var response = restClient.Get<List<Pessoa>>(request);
            ViewBag.ListaPessoas = response.Data;

            return View();
        }

        // POST: Amigo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Amigo amigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/amigos", DataFormat.Json);
            request.AddJsonBody(amigo);
            var response = restClient.Post<Pessoa>(request);

            return Redirect("/amigo/index");
        }

        // GET: Amigo/Delete/5
        public ActionResult Delete(int id)
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/amigos/" + id, DataFormat.Json);
            var response = restClient.Get<Amigo>(request);

            return View(response.Data);
        }

        // POST: Amigo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var restClient = new RestClient();

                var request = new RestRequest("http://localhost:5000/api/amigos/" + id, DataFormat.Json);
                var response = restClient.Delete<Amigo>(request);

                return Redirect("/amigo/index");
            }
            catch
            {
                return View();
            }
        }
    }
}