using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.WebMVC.Models.PaisModel;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RestSharp;

namespace AT.WebMVC.Controllers
{
    public class PaisController : Controller
    {
        // GET: Pais
        public ActionResult Index()
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/paises", DataFormat.Json);
            var response = restClient.Get<List<Pais>>(request);

            return View(response.Data);
        }

        // GET: Pais/Details/5
        public ActionResult Details(int id)
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/paises/" + id, DataFormat.Json);
            var response = restClient.Get<Pais>(request);

            return View(response.Data);
        }

        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CriarPaisViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var restClient = new RestClient();
            var request = new RestRequest("http://localhost:5000/api/paises", DataFormat.Json);
            viewModel.Bandeira = UploadFoto(viewModel.BandeiraArq);
            request.AddJsonBody(viewModel);
            var response = restClient.Post<CriarPaisViewModel>(request);

            return Redirect("/pais/index");
        }

        // GET: Pais/Edit/5
        public ActionResult Edit(int id)
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/paises/" + id, DataFormat.Json);
            var response = restClient.Get<Pais>(request);

            return View(response.Data);
        }

        // POST: Pais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pais pais)
        {
            try
            {
                var restClient = new RestClient();

                var request = new RestRequest("http://localhost:5000/api/paises/" + id, DataFormat.Json);
                request.AddJsonBody(pais);
                var response = restClient.Put<Pais>(request);

                return Redirect("/pais/index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pais/Delete/5
        public ActionResult Delete(int id)
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/paises/" + id, DataFormat.Json);
            var response = restClient.Get<Pais>(request);

            return View(response.Data);
        }

        // POST: Pais/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pais pais)
        {
            try
            {
                var restClient = new RestClient();

                var request = new RestRequest("http://localhost:5000/api/paises/" + id, DataFormat.Json);
                var response = restClient.Delete<Pais>(request);

                return Redirect("/pais/index");
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