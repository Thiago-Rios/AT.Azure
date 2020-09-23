using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AT.WebMVC.Models.PaisModel
{
    public class CriarPaisViewModel
    {
        public string Nome { get; set; }
        public IFormFile BandeiraArq { get; set; }
        public string Bandeira { get; set; }
    }
}
