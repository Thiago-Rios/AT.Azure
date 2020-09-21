using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        [JsonIgnore]
        public Pessoa AmigoPessoa { get; set; }
    }
}
