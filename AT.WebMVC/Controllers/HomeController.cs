using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AT.WebMVC.Models;
using AT.WebMVC.Models.HomeModel;
using RestSharp;
using Domain;

namespace AT.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ListaQuantidadeViewModel viewModel = new ListaQuantidadeViewModel();

            var restClient = new RestClient();

            var requestPessoa = new RestRequest("http://localhost:5000/api/pessoas", DataFormat.Json);
            var responsePessoa = restClient.Get<List<Pessoa>>(requestPessoa);
            viewModel.QntPessoas = responsePessoa.Data.Count;

            var requestPais = new RestRequest("http://localhost:5000/api/paises", DataFormat.Json);
            var responsePais = restClient.Get<List<Pais>>(requestPais);
            viewModel.QntPaises = responsePais.Data.Count;

            var requestEstado = new RestRequest("http://localhost:5000/api/estados", DataFormat.Json);
            var responseEstado = restClient.Get<List<Estado>>(requestEstado);
            viewModel.QntEstados = responseEstado.Data.Count;

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
