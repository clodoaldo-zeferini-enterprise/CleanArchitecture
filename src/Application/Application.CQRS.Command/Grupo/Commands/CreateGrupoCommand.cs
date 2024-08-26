using Domain.Entities;
using NetCore.Base.Enum;

namespace Application.CQRS.Command.Grupo.Commands;
public class CreateGrupoCommand : GrupoCommandBase
{
    public string NomeDoGrupo { get; private set; }
    public string RazaoSocial { get; private set; }
    public string NomeFantasia { get; private set; }
    public string Cnpj { get; private set; }
    public string InscricaoEstadual { get; private set; }
    public string CpfDoAdministrador { get; private set; }
    public string PreNomeDoAdministrador { get; private set; }
    public string NomeDoMeioDoAdministrador { get; private set; }
    public string SobreNomeDoAdministrador { get; private set; }
    public string EmailDoAdministrador { get; private set; }
    public string InsertedBy { get; private set; }

    public CreateGrupoCommand(string nomeDoGrupo, string razaoSocial, string nomeFantasia, string cnpj, string inscricaoEstadual, string cpfDoAdministrador, string preNomeDoAdministrador, string nomeDoMeioDoAdministrador, string sobreNomeDoAdministrador, string emailDoAdministrador, string insertedBy)
    {
        NomeDoGrupo = nomeDoGrupo;
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
        Cnpj = cnpj;
        InscricaoEstadual = inscricaoEstadual;
        CpfDoAdministrador = cpfDoAdministrador;
        PreNomeDoAdministrador = preNomeDoAdministrador;
        NomeDoMeioDoAdministrador = nomeDoMeioDoAdministrador;
        SobreNomeDoAdministrador = sobreNomeDoAdministrador;
        EmailDoAdministrador = emailDoAdministrador;
        InsertedBy = insertedBy;
    }

    public Domain.Entities.Grupo CreateEntity()
    {
        return new Domain.Entities.Grupo(
               NomeDoGrupo
            ,  RazaoSocial
            ,  NomeFantasia
            ,  Cnpj
            ,  InscricaoEstadual
            ,  CpfDoAdministrador
            ,  PreNomeDoAdministrador
            ,  NomeDoMeioDoAdministrador
            ,  SobreNomeDoAdministrador
            ,  EmailDoAdministrador
            ,  InsertedBy
        );
    }
}
