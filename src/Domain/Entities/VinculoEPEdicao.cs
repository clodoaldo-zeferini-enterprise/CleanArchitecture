
using NetCore.Base.Enum; namespace Domain.Entities
{
    public class VinculoEPEdicao
    {
        public string Ori_NomeDoGrupo { get;  private set; }
        public string Ori_NomeDaEmpresa { get;  private set; }
        public string Ori_NomeDaFilial { get;  private set; }
        public string Ori_NomeDoDepartamento { get;  private set; }
        public string Ori_NomeDaPessoa { get;  private set; }
        public string Ori_NomeDoUsuario { get;  private set; }
        public short Ori_Nivel { get;  private set; }
        public short Ori_Perfil { get;  private set; }
        public short Ori_Status { get;  private set; }
        public string Des_NomeDoGrupo { get;  private set; }
        public string Des_NomeDaEmpresa { get;  private set; }
        public string Des_NomeDaFilial { get;  private set; }
        public string Des_NomeDoDepartamento { get;  private set; }
        public string Des_NomeDaPessoa { get;  private set; }
        public string Des_NomeDoUsuario { get;  private set; }
        public short Des_Nivel { get;  private set; }
        public short Des_Perfil { get;  private set; }
        public short Des_Status { get;  private set; }
    }
}