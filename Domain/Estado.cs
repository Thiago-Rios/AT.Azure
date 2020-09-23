using System.Text.Json.Serialization;

namespace Domain
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Bandeira { get; set; }
        public Pais Pais { get; set; }
    }
}
