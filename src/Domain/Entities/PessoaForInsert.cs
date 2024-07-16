using NetCore.Base.Enum; namespace Domain.Entities
{
    public class PessoaForInsert
    {
        public Guid DepartamentoId { get;  private set; }
        public Guid PessoaId { get;  private set; }
        public string NomeDaPessoa { get;  private set; }
        public EStatus Status { get;  private set; }
        public short Nivel { get;  private set; }
        public short Perfil { get;  private set; }
        public string Email { get;  private set; }
        public string Senha { get;  private set; }
        public string ConfirmaSenha { get;  private set; }
        public string CPF { get;  private set; }
    }
}
