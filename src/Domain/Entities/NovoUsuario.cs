using NetCore.Base.Enum; namespace Domain.Entities
{
    public class NovoUsuario
    {
        public Guid NovoUsuarioId { get;  private set; }
        public string NomeCompleto { get;  private set; }
        public string CPF { get;  private set; }
        public string CEP { get;  private set; }
        public string Endereco { get;  private set; }
        public string Numero { get;  private set; }
        public string Complemento { get;  private set; }
        public string Bairro { get;  private set; }
        public string Cidade { get;  private set; }
        public string UF { get;  private set; }
        public string TelResidencial { get;  private set; }
        public string TelComercial { get;  private set; }
        public string TelCelular { get;  private set; }
        public string TelObservacao { get;  private set; }
        public string Email { get;  private set; }
        public string PrimeiroNome { get;  private set; }
        public string UltimoSobreNome { get;  private set; }
        public Guid DepartamentoId { get;  private set; }
        public string TipoDeLogradouro { get;  private set; }
    }
}
