using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.WebMVC.Models.EstadoModel;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RestSharp;

namespace AT.WebMVC.Controllers
{
    public class EstadoController : Controller
    {
        public ActionResult Index()
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/estados", DataFormat.Json);
            var response = restClient.Get<List<Estado>>(request);

            return View(response.Data);
        }

        // GET: Estado/Details/5
        public ActionResult Details(int id)
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/estados/" + id, DataFormat.Json);
            var response = restClient.Get<Estado>(request);

            return View(response.Data);
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/paises", DataFormat.Json);
            var response = restClient.Get<List<Pais>>(request);
            ViewBag.ListaPaises = response.Data;

            return View();
        }

        // POST: Estado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CriarEstadoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var restClient = new RestClient();
            var request = new RestRequest("http://localhost:5000/api/estados", DataFormat.Json);
            viewModel.Bandeira = UploadFoto(viewModel.BandeiraArq);
            request.AddJsonBody(viewModel);
            var response = restClient.Post<CriarEstadoViewModel>(request);

            return Redirect("/estado/index");
        }

        // GET: Estado/Edit/5
        public ActionResult Edit(int id)
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/estados/" + id, DataFormat.Json);
            var response = restClient.Get<Estado>(request);

            return View(response.Data);
        }

        // POST: Estado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Estado estado)
        {
            try
            {
                var restClient = new RestClient();

                var request = new RestRequest("http://localhost:5000/api/estados/" + id, DataFormat.Json);
                request.AddJsonBody(estado);
                var response = restClient.Put<Estado>(request);

                return Redirect("/estado/index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Estado/Delete/5
        public ActionResult Delete(int id)
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/estados/" + id, DataFormat.Json);
            var response = restClient.Get<Estado>(request);

            return View(response.Data);
        }

        // POST: Estado/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var restClient = new RestClient();

                var request = new RestRequest("http://localhost:5000/api/estados/" + id, DataFormat.Json);
                var response = restClient.Delete<Estado>(request);

                return Redirect("/estado/index");
            }
            catch
            {
                return View();
            }
        }

        private string UploadFoto(IFormFile foto)
        {
            var reader = foto.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=thiagoriosatazure;AccountKey=bagMdX+v17w6N64t9dZBM6zuC9MEOi4Wxgb/Lgb1z9bNUxoOsC+b/wRpqC8fibGBDfuFyTMk+cTdBd2g79yvMw==;EndpointSuffix=core.windows.net");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("foto-amigo");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();
            return destinoDaImagemNaNuvem;
        }
    }
}