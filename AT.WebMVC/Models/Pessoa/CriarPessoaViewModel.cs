using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AT.WebMVC.Models.Pessoa
{
    public class CriarPessoaViewModel
    {
        public string Nome { get; set; }
        public IFormFile FotoArq { get; set; }
        public string Foto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
