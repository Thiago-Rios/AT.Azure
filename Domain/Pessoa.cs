using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual IList<Amigo> Amigos { get; set; }
    }
}
