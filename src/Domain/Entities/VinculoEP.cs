using NetCore.Base.Enum; namespace Domain.Entities
{
    public class VinculoEP
    {
        public Guid VinculoEPId { get;  private set; }
        public EStatus Status { get;  private set; }
        public Guid SysUsuSessionId { get;  private set; }
        public Guid Ori_GrupoId { get;  private set; }
        public string Ori_NomeDoGrupo { get;  private set; }
        public Guid Ori_EmpresaId { get;  private set; }
        public string Ori_NomeDaEmpresa { get;  private set; }
        public Guid Ori_FilialId { get;  private set; }
        public string Ori_NomeDaFilial { get;  private set; }
        public Guid Ori_DepartamentoId { get;  private set; }
        public string Ori_NomeDoDepartamento { get;  private set; }
        public Guid Ori_PessoaId { get;  private set; }
        public string Ori_NomeDaPessoa { get;  private set; }
        public Guid Ori_UsuarioId { get;  private set; }
        public string Ori_NomeDoUsuario { get;  private set; }
        public short Ori_Nivel { get;  private set; }
        public short Ori_Perfil { get;  private set; }
        public short Ori_Status { get;  private set; }
        public Guid Des_GrupoId { get;  private set; }
        public string Des_NomeDoGrupo { get;  private set; }
        public Guid Des_EmpresaId { get;  private set; }
        public string Des_NomeDaEmpresa { get;  private set; }
        public Guid Des_FilialId { get;  private set; }
        public string Des_NomeDaFilial { get;  private set; }
        public Guid Des_DepartamentoId { get;  private set; }
        public string Des_NomeDoDepartamento { get;  private set; }
        public Guid Des_PessoaId { get;  private set; }
        public string Des_NomeDaPessoa { get;  private set; }
        public Guid Des_UsuarioId { get;  private set; }
        public string Des_NomeDoUsuario { get;  private set; }
        public short Des_Nivel { get;  private set; }
        public short Des_Perfil { get;  private set; }
        public short Des_Status { get;  private set; }

    }
}