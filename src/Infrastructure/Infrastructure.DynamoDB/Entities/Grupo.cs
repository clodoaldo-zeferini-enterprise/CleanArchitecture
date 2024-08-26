using Amazon.DynamoDBv2.DataModel;
using NetCore.Base;
using Domain.Validation;
using Domain.Enums;
using NetCore.Base.Enum;
using System.Reflection;
using System;
using Domain.Builders;
using Domain.Entities;
using System.Xml.Linq;

namespace Infrastructure.DynamoDB.Entities;

[DynamoDBTable("Grupos")] public sealed class Grupo : Root
{
    [DynamoDBHashKey]  public string Id { get; private set; }
    [DynamoDBProperty] public ICollection<Infrastructure.DynamoDB.Entities.Empresa>? Empresas { get; private set; }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="grupo"></param>
    public Grupo(Domain.Entities.Grupo grupo) : base(grupo.Name, grupo.InsertedBy)
    {
        Id = grupo.Id;

        if (grupo.Empresas != null && grupo.Empresas.Count > 0)
        {   
           
            foreach (var empresa in grupo.Empresas)
            {
                Infrastructure.DynamoDB.Entities.Empresa empresaDynamoDB = new Infrastructure.DynamoDB.Entities.Empresa(empresa);
                if (empresa.Filiais != null && empresa.Filiais.Count > 0)
                {
                    foreach (var filial in empresa.Filiais)
                    {
                        Infrastructure.DynamoDB.Entities.Filial filialDynamoDB = new Infrastructure.DynamoDB.Entities.Filial(filial);
                        if (filial.Departamentos != null && filial.Departamentos.Count > 0)
                        {
                            foreach (var departamento in filial.Departamentos)
                            {
                                Infrastructure.DynamoDB.Entities.Departamento departamentoDynamoDB = new Infrastructure.DynamoDB.Entities.Departamento(departamento);

                                if (departamento.Pessoas != null && departamento.Pessoas.Count > 0)
                                {
                                    foreach (var pessoa in departamento.Pessoas)
                                    {
                                        Infrastructure.DynamoDB.Entities.Pessoa pessoaDynamoDB = new Infrastructure.DynamoDB.Entities.Pessoa(pessoa);

                                        if (pessoa.Usuarios != null && pessoa.Usuarios.Count > 0)
                                        {
                                            foreach (var usuario in pessoa.Usuarios)
                                            {

                                                Infrastructure.DynamoDB.Entities.Usuario usuarioDynamoDB = new Infrastructure.DynamoDB.Entities.Usuario(usuario);

                                                if (usuario.Direitos != null && usuario.Direitos.Count > 0)
                                                {                                                    
                                                    foreach (var direito in usuario.Direitos)
                                                    {
                                                        
                                                    }
                                                }

                                                pessoaDynamoDB.AddUsuario(usuarioDynamoDB);
                                            }
                                        }

                                        departamentoDynamoDB.AddPessoa(pessoaDynamoDB);
                                    }
                                }
                                
                                filialDynamoDB.AddDepartamento(departamentoDynamoDB);
                            }
                        }

                        empresaDynamoDB.AddFilial(filialDynamoDB);                        
                    }
                }

                AddEmpresa(empresaDynamoDB);
            }
        }       
    }

    public void AddEmpresa(Empresa empresa)
    {
        Empresas ??= new List<Empresa>();
        Empresas.Add(empresa);
    }
}

[DynamoDBTable("Empresas")] public sealed class Empresa : NetCore.Base.PessoaJuridica
{
    [DynamoDBHashKey]  public string Id { get; private set; }
    [DynamoDBProperty] public string GrupoId { get; private set; }
    [DynamoDBProperty] public string NomeFantasia { get; private set; }
    [DynamoDBProperty] public string DepartamentoSysMegaId { get; private set; }
    [DynamoDBProperty] public string PessoaAdministradorId { get; private set; }
    [DynamoDBProperty] public string CPFDoAdministrador { get; private set; }
    [DynamoDBProperty] public string NomeDoAdministrador { get; private set; }
    [DynamoDBProperty] public string EmailDoAdministrador { get; private set; }
    [DynamoDBProperty] public string PessoaUsuarioId { get; private set; }
    [DynamoDBProperty] public ICollection<Filial> Filiais { get; private set; }
    [DynamoDBProperty] public ICollection<Sistema> Sistemas { get; private set; }
        
    /// <summary>
    /// Create
    /// </summary>
    /// <param name="empresa"></param>
    public Empresa(Domain.Entities.Empresa empresa)
        : base(empresa.CNPJ, empresa.InscricaoEstadual, empresa.NomeFantasia, empresa.RazaoSocial, empresa.Status, empresa.InsertedOn, empresa.InsertedBy, empresa.UpdatedBy)
    {
        GrupoId = empresa.GrupoId;
        Id = empresa.Id;
    }

    public void AddFilial(Filial filial)
    {
        Filiais ??= new List<Filial>();
        Filiais.Add(filial);
    }

    public void AddSistema(Sistema sistema)
    {
        Sistemas ??= new List<Sistema>();
        Sistemas.Add(sistema);
    }
}

[DynamoDBTable("Filiais")] public sealed class Filial : NetCore.Base.PessoaJuridica
{
    [DynamoDBProperty] public string EmpresaId { get; private set; }
    [DynamoDBHashKey]  public string Id { get; private set; }
    [DynamoDBProperty] public EStatus Status { get; private set; }
    [DynamoDBProperty] public string CNPJ { get; private set; }
    [DynamoDBProperty] public string InscricaoEstadual { get; private set; }
    [DynamoDBProperty] public string RazaoSocial { get; private set; }
    [DynamoDBProperty] public string NomeDaFilial { get; private set; }
    [DynamoDBProperty] public ICollection<Departamento> Departamentos { get; private set; }
    
    /// <summary>
    /// Create
    /// </summary>
    /// <param name="filial"></param>
    public Filial(Domain.Entities.Filial filial)
        : base(filial.CNPJ, filial.InscricaoEstadual, filial.NomeDaFilial, filial.RazaoSocial, filial.Status, filial.InsertedOn, filial.InsertedBy, filial.UpdatedBy)
    {
        EmpresaId = filial.EmpresaId;
        Id = filial.Id;
    }

    public void AddDepartamento(Departamento departamento)
    {
        Departamentos ??= new List<Departamento>();
        Departamentos.Add(departamento);
    }
}


[DynamoDBTable("Departamentos")] public sealed class Departamento : Root
{
    [DynamoDBProperty] public string FilialId { get; private set; }
    [DynamoDBHashKey]  public string Id { get; private set; }
    [DynamoDBProperty] public ICollection<Pessoa>? Pessoas { get; private set; }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="departamento"></param>
    public Departamento(Domain.Entities.Departamento departamento) :
        base(departamento.Name, departamento.Status, departamento.InsertedOn, departamento.InsertedBy, departamento.UpdatedBy)
    {
        FilialId = departamento.FilialId;
        Id = departamento.Id;
    }

    public void AddPessoa(Pessoa pessoa)
    {
        Pessoas ??= new List<Pessoa>();
        Pessoas.Add(pessoa);
    }
}

[DynamoDBTable("Pessoas")] public sealed class Pessoa : PessoaFisica
{
    [DynamoDBProperty] public string DepartamentoId { get; private set; }
    [DynamoDBProperty] public ICollection<Usuario>? Usuarios { get; private set; }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="pessoa"></param>
    public Pessoa(Domain.Entities.Pessoa pessoa)
        : base(pessoa.Id, pessoa.CPF, pessoa.PreNome, pessoa.NomeDoMeio, pessoa.SobreNome, pessoa.Status, pessoa.InsertedOn, pessoa.InsertedBy, pessoa.UpdatedBy)
    {
        DepartamentoId = pessoa.DepartamentoId;
    }

    public void AddUsuario(Usuario usuario)
    {
        Usuarios ??= new List<Usuario>();
        Usuarios.Add(usuario);
    }

    public void AddUsuario(ICollection<Usuario> usuario)
    {
        NetCore.Base.ValidadorDeRegra
        .Novo()
        .Quando((usuario == null), "usuario is required")
        .DispararExcecaoSeExistir();

        Usuarios ??= new List<Usuario>();
        Usuarios = usuario;
    }
}

[DynamoDBTable("Usuarios")] public sealed class Usuario : Root
{
    [DynamoDBProperty] public string PessoaId { get; private set; }
    [DynamoDBHashKey]  public string Id { get; private set; }
    public short PerfilNumero { get; private set; }
    public short NivelNumero { get; private set; }
    [DynamoDBProperty] public string Email { get; private set; }
    [DynamoDBProperty] public string Senha { get; private set; }
    public long SessoesAtivas { get; private set; }
    public long NumeroMaximoDeSessoesAtivas { get; private set; }
    [DynamoDBProperty] public ICollection<Direito>? Direitos { get; private set; }


    /// <summary>
    /// Create
    /// </summary>
    /// <param name="usuario"></param>
    public Usuario(Domain.Entities.Usuario usuario)
        : base(usuario.Name, usuario.Status, usuario.InsertedOn, usuario.InsertedBy, usuario.UpdatedBy)
    {
        Id = usuario.Id;
        PessoaId = usuario.PessoaId;
        PerfilNumero = usuario.PerfilNumero;
        NivelNumero = usuario.NivelNumero;
        Email = usuario.Email;
        Senha = usuario.Senha;
        NumeroMaximoDeSessoesAtivas = usuario.NumeroMaximoDeSessoesAtivas;
    }
}

