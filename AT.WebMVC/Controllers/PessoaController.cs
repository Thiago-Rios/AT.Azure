using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.WebMVC.Models.Pessoa;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RestSharp;

namespace AT.WebMVC.Controllers
{
    public class PessoaController : Controller
    {
        // GET: Pessoa
        public ActionResult Index()
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/pessoas", DataFormat.Json);
            var response = restClient.Get<List<Pessoa>>(request);

            return View(response.Data);
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(int id)
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/pessoas/" + id, DataFormat.Json);
            var response = restClient.Get<Pessoa>(request);

            return View(response.Data);
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            var restClient = new RestClient();
            var request = new RestRequest("http://localhost:5000/api/paises", DataFormat.Json);
            var response = restClient.Get<List<Pais>>(request);
            ViewBag.ListaPaises = response.Data;

            var restEstado = new RestClient();
            var requestEstado = new RestRequest("http://localhost:5000/api/estados", DataFormat.Json);
            var responseEstado = restEstado.Get<List<Estado>>(requestEstado);
            ViewBag.ListaEstados = responseEstado.Data;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CriarPessoaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var restClient = new RestClient();
            var request = new RestRequest("http://localhost:5000/api/pessoas", DataFormat.Json);
            viewModel.Foto = UploadFoto(viewModel.FotoArq);
            request.AddJsonBody(viewModel);
            var response = restClient.Post<CriarPessoaViewModel>(request);

            return Redirect("/pessoa/index");
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(int id)
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/pessoas/" + id, DataFormat.Json);
            var response = restClient.Get<Pessoa>(request);

            return View(response.Data);
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pessoa pessoa)
        {
            try
            {
                var restClient = new RestClient();

                var request = new RestRequest("http://localhost:5000/api/pessoas/" + id, DataFormat.Json);
                request.AddJsonBody(pessoa);
                var response = restClient.Put<Pessoa>(request);

                return Redirect("/pessoa/index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(int id)
        {
            var restClient = new RestClient();

            var request = new RestRequest("http://localhost:5000/api/pessoas/" + id, DataFormat.Json);
            var response = restClient.Get<Pessoa>(request);

            return View(response.Data);
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var restClient = new RestClient();

                var request = new RestRequest("http://localhost:5000/api/pessoas/" + id, DataFormat.Json);
                var response = restClient.Delete<Pessoa>(request);

                return Redirect("/pessoa/index");
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