using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Pais
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Bandeira { get; set; }
        [JsonIgnore]
        public List<Estado> Estados { get; set; }
    }
}
