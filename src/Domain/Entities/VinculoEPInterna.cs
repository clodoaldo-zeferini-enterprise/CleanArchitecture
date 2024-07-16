
using NetCore.Base.Enum; namespace Domain.Entities
{
    public class VinculoEPInterna
    {
        public EStatus Status { get;  private set; }
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
        public Guid UsuarioId { get;  private set; }
        public string NomeDoUsuario { get;  private set; }    
    }
}