using NetCore.Base.Enum; namespace Domain.Entities
{
    public class PessoaChaveCompleta
    {
        public Guid SistemaId { get;  private set; }
        public string NomeDoSistema { get;  private set; }
        public Guid GrupoId { get;  private set; }
        public string NomeDoGrupo { get;  private set; }
        public Guid EmpresaId { get;  private set; }
        public string NomeDaEmpresa { get;  private set; }
        public Guid FilialId { get;  private set; }
        public string NomeDaFilial { get;  private set; }
        public Guid DepartamentoId { get;  private set; }
        public string NomeDoDepartamento { get;  private set; }
        public Guid PessoaId { get;  private set; }
        public string NomeDaPessoa { get;  private set; }
        public short NivelNumero { get;  private set; }
        public short PerfilNumero { get;  private set; }
        public string Login { get;  private set; }
        public string Email { get;  private set; }
        public string CPF { get;  private set; }
        public Guid PessoaChaveCompletaId { get;  private set; }
        public Guid UsuarioId { get;  private set; }
        public int CodigoDaPessoa { get;  private set; }
        public EStatus Status { get;  private set; }
    }
}
