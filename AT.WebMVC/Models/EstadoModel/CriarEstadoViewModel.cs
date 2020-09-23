using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AT.WebMVC.Models.EstadoModel
{
    public class CriarEstadoViewModel
    {
        public string Nome { get; set; }
        public IFormFile BandeiraArq { get; set; }
        public string Bandeira { get; set; }
        public Pais Pais { get; set; }
    }
}
