using NetCore.Base.Enum; 
namespace Domain.Entities
{
    public class Endereco
    {
        public Guid EnderecoDe { get; set; }
        public Guid EnderecoId { get; set; }
        public EStatus Status { get; set; }
        public string TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
    }
}
