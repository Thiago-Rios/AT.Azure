namespace Domain
{
    public class AmigoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Pessoa AmigoPessoa { get; set; }
    }
}
