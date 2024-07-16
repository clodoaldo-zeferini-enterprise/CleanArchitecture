using NetCore.Base.Enum; namespace Domain.Entities
{
    public class Contato
    {
        public Guid ContatoDe { get; set; }
        public Guid ContatoId { get; set; }
        public EStatus Status { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public short TipoDeContato { get; set; }
    }
}
