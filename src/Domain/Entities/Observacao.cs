using NetCore.Base.Enum; namespace Domain.Entities
{
    public class Observacao
    {
        public Guid ObservacaoDe { get;  private set; }
        public Guid ObservacaoId { get;  private set; }
        public EStatus Status { get;  private set; }
        public string ConteudoDaObservacao { get;  private set; }
    }
}
