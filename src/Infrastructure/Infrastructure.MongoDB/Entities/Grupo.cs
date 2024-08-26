using NetCore.Base.Enum;
using Domain.Validation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections;

namespace Infrastructure.MongoDB.Entities;

public sealed class Grupo : Domain.Entities.Grupo
{
    [BsonId] public ObjectId _id { get; set; }
    
   
    /// <summary>
    /// Create
    /// </summary>
    /// <param name="nomeDoGrupo"></param>
    /// <param name="razaoSocial"></param>
    /// <param name="nomeFantasia"></param>
    /// <param name="cnpj"></param>
    /// <param name="inscricaoEstadual"></param>
    /// <param name="cpfDoAdministrador"></param>
    /// <param name="preNomeDoAdministrador"></param>
    /// <param name="nomeDoMeioDoAdministrador"></param>
    /// <param name="sobreNomeDoAdministrador"></param>
    /// <param name="emailDoAdministrador"></param>
    /// <param name="insertedBy"></param>
    public Grupo(
          string nomeDoGrupo, string razaoSocial, string nomeFantasia
        , string cnpj, string inscricaoEstadual
        , string cpfDoAdministrador, string preNomeDoAdministrador, string nomeDoMeioDoAdministrador, string sobreNomeDoAdministrador, string emailDoAdministrador
        , string insertedBy) : base (nomeDoGrupo, razaoSocial, nomeFantasia, cnpj, inscricaoEstadual, cpfDoAdministrador, preNomeDoAdministrador, nomeDoMeioDoAdministrador, sobreNomeDoAdministrador, emailDoAdministrador, insertedBy)
    {}

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="id"></param>
    /// <param name="nomeDoGrupo"></param>
    /// <param name="razaoSocial"></param>
    /// <param name="nomeFantasia"></param>
    /// <param name="cnpj"></param>
    /// <param name="inscricaoEstadual"></param>
    /// <param name="cpfDoAdministrador"></param>
    /// <param name="preNomeDoAdministrador"></param>
    /// <param name="nomeDoMeioDoAdministrador"></param>
    /// <param name="sobreNomeDoAdministrador"></param>
    /// <param name="emailDoAdministrador"></param>
    /// <param name="status"></param>
    /// <param name="insertedOn"></param>
    /// <param name="insertedBy"></param>
    /// <param name="updatedOn"></param>
    /// <param name="updatedBy"></param>
    /// <param name="empresas"></param>
    public Grupo(string id, string nomeDoGrupo, string razaoSocial, string nomeFantasia
        , string cnpj, string inscricaoEstadual
        , string cpfDoAdministrador, string preNomeDoAdministrador, string nomeDoMeioDoAdministrador, string sobreNomeDoAdministrador, string emailDoAdministrador
        , EStatus status, DateTime insertedOn, string insertedBy, DateTime updatedOn, string updatedBy, ICollection<Domain.Entities.Empresa> empresas)
        : base(id, nomeDoGrupo, razaoSocial, nomeFantasia, cnpj, inscricaoEstadual, cpfDoAdministrador, preNomeDoAdministrador, nomeDoMeioDoAdministrador, sobreNomeDoAdministrador
            , emailDoAdministrador, status, insertedOn, insertedBy, updatedOn, updatedBy, empresas)
    {}
}

public sealed class Empresa : Domain.Entities.Empresa
{
    [BsonId] public ObjectId _id { get; set; }
    

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="grupoId"></param>
    /// <param name="cnpj"></param>
    /// <param name="inscricaoEstadual"></param>
    /// <param name="nomeFantasia"></param>
    /// <param name="razaoSocial"></param>
    /// <param name="nomeDoAdministrador"></param>
    /// <param name="emailDoAdministrador"></param>
    /// <param name="insertedBy"></param>
    public Empresa(
        string grupoId, string cnpj, string inscricaoEstadual, string nomeFantasia, string razaoSocial,
        string cpfDoAdministrador, string preNomeDoAdministrador, string nomeDoMeioDoAdministrador, string sobreNomeDoAdministrador, string emailDoAdministrador, string insertedBy)
        : base(grupoId, cnpj, inscricaoEstadual, nomeFantasia, razaoSocial, cpfDoAdministrador, preNomeDoAdministrador, nomeDoMeioDoAdministrador, sobreNomeDoAdministrador, emailDoAdministrador, insertedBy)
    {
        /*
        Filial filial = new Filial(Id, CNPJ, InscricaoEstadual, NomeFantasia, RazaoSocial, insertedBy);
        Departamento departamento = new Departamento(filial.Id, DepartamentoSysMegaId, "Administradores", insertedBy);
        Pessoa pessoa = new Pessoa(departamento.Id, PessoaAdministradorId, CPFDoAdministrador, preNomeDoAdministrador, nomeDoMeioDoAdministrador, sobreNomeDoAdministrador, insertedBy);
        Usuario usuario = new Usuario(pessoa.Id, NomeDoAdministrador, 1, 1, emailDoAdministrador, "123mudar", 10, insertedBy);

        pessoa.AddUsuario(usuario);
        departamento.AddPessoa(pessoa);
        filial.AddDepartamento(departamento);
        AddFilial(filial);
        */
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="grupoId"></param>
    /// <param name="id"></param>
    /// <param name="cnpj"></param>
    /// <param name="inscricaoEstadual"></param>
    /// <param name="nomeFantasia"></param>
    /// <param name="razaoSocial"></param>
    /// <param name="status"></param>
    /// <param name="insertedOn"></param>
    /// <param name="insertedBy"></param>
    /// <param name="updatedBy"></param>
    public Empresa(string grupoId, string id, string cnpj, string inscricaoEstadual, string nomeFantasia, string razaoSocial, EStatus status, DateTime insertedOn, string insertedBy, string updatedBy)
        : base(grupoId, id, cnpj, inscricaoEstadual, nomeFantasia, razaoSocial, status, insertedOn, insertedBy, updatedBy)
    {
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="cnpj"></param>
    /// <param name="inscricaoEstadual"></param>
    /// <param name="nomeFantasia"></param>
    /// <param name="razaoSocial"></param>
    /// <param name="insertedBy"></param>
    public Empresa(string grupoId, string id, string cnpj, string inscricaoEstadual, string nomeFantasia, string razaoSocial, string insertedBy, DateTime insertedOn, string updatedBy, DateTime updatedOn)
        : base(grupoId, id, cnpj, inscricaoEstadual, nomeFantasia, razaoSocial, insertedBy, insertedOn, updatedBy, updatedOn)
    {
    }   
}

public sealed class Filial : Domain.Entities.Filial
{
    [BsonId] public ObjectId _id { get; set; }


    /// <summary>
    /// Create
    /// </summary>
    /// <param name="empresaId"></param>
    /// <param name="cnpj"></param>
    /// <param name="inscricaoEstadual"></param>
    /// <param name="nome"></param>
    /// <param name="razaoSocial"></param>
    /// <param name="insertedBy"></param>
    public Filial(string empresaId, string cnpj, string inscricaoEstadual, string nome, string razaoSocial, string insertedBy)
        : base (empresaId, cnpj, inscricaoEstadual, nome, razaoSocial, insertedBy)
    {
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="empresaId"></param>
    /// <param name="id"></param>
    /// <param name="cnpj"></param>
    /// <param name="inscricaoEstadual"></param>
    /// <param name="nome"></param>
    /// <param name="razaoSocial"></param>
    /// <param name="status"></param>
    /// <param name="insertedOn"></param>
    /// <param name="insertedBy"></param>
    /// <param name="updatedBy"></param>
    public Filial(string empresaId, string id, string cnpj, string inscricaoEstadual, string nome, string razaoSocial, EStatus status, DateTime insertedOn, string insertedBy, string updatedBy)
        : base(empresaId, id, cnpj, inscricaoEstadual, nome, razaoSocial, status, insertedOn, insertedBy, updatedBy)
    {
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="cnpj"></param>
    /// <param name="inscricaoEstadual"></param>
    /// <param name="nomeFantasia"></param>
    /// <param name="razaoSocial"></param>
    /// <param name="status"></param>
    /// <param name="insertedOn"></param>
    /// <param name="updatedOn"></param>
    /// <param name="insertedBy"></param>
    /// <param name="updatedBy"></param>
    public Filial(string empresaId, string id, string cnpj, string inscricaoEstadual, string nomeFantasia, string razaoSocial, EStatus status, DateTime insertedOn, DateTime updatedOn, string insertedBy, string updatedBy)
        : base(empresaId, id, cnpj, inscricaoEstadual, nomeFantasia, razaoSocial, status, insertedOn, updatedOn, insertedBy, updatedBy)
    {
    }
}

public sealed class Departamento : Domain.Entities.Departamento
{
    [BsonId] public ObjectId _id { get; set; }
    
    /// <summary>
    /// Create
    /// </summary>
    /// <param name="filialId"></param>
    /// <param name="departamentoId"></param>
    /// <param name="name"></param>
    /// <param name="insertedBy"></param>
    public Departamento(string filialId, string departamentoId, string name, string insertedBy) 
        : base(filialId, departamentoId, name, insertedBy)
    {
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="filialId"></param>
    /// <param name="departamentoId"></param>
    /// <param name="name"></param>
    /// <param name="status"></param>
    /// <param name="insertedOn"></param>
    /// <param name="insertedBy"></param>
    /// <param name="updatedBy"></param>
    public Departamento(string filialId, string departamentoId, string name, EStatus status, DateTime insertedOn, string insertedBy, string updatedBy) :
        base(filialId, departamentoId, name, status, insertedOn, insertedBy, updatedBy)
    {}

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="filialId"></param>
    /// <param name="departamentoId"></param>
    /// <param name="name"></param>
    /// <param name="status"></param>
    /// <param name="insertedOn"></param>
    /// <param name="insertedBy"></param>
    /// <param name="updatedBy"></param>
    public Departamento(string filialId, string departamentoId, string name, EStatus status, DateTime insertedOn, DateTime updatedOn, string insertedBy, string updatedBy) :
        base(filialId, departamentoId, name, status, insertedOn, updatedOn, insertedBy, updatedBy)
    {}  
}

public sealed class Pessoa : Domain.Entities.Pessoa
{
    /// <summary>
    /// Create
    /// </summary>
    /// <param name="departamentoId"></param>
    /// <param name="pessoaId"></param>
    /// <param name="cpf"></param>
    /// <param name="preNome"></param>
    /// <param name="nomeDoMeio"></param>
    /// <param name="sobreNome"></param>
    /// <param name="insertedBy"></param>
    public Pessoa(string departamentoId, string pessoaId, string cpf, string preNome, string nomeDoMeio, string sobreNome, string insertedBy)
        : base(departamentoId, pessoaId, cpf, preNome, nomeDoMeio, sobreNome, insertedBy)
    {
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="departamentoId"></param>
    /// <param name="pessoaId"></param>
    /// <param name="cpf"></param>
    /// <param name="preNome"></param>
    /// <param name="nomeDoMeio"></param>
    /// <param name="sobreNome"></param>
    /// <param name="status"></param>
    /// <param name="insertedOn"></param>
    /// <param name="insertedBy"></param>
    /// <param name="updatedBy"></param>
    public Pessoa(string departamentoId, string pessoaId, string cpf, string preNome, string nomeDoMeio, string sobreNome, EStatus status, DateTime insertedOn, string insertedBy, string updatedBy)
        : base(departamentoId, pessoaId, cpf, preNome, nomeDoMeio, sobreNome, status, insertedOn, insertedBy, updatedBy)
    {
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="departamentoId"></param>
    /// <param name="pessoaId"></param>
    /// <param name="cpf"></param>
    /// <param name="preNome"></param>
    /// <param name="nomeDoMeio"></param>
    /// <param name="sobreNome"></param>
    /// <param name="status"></param>
    /// <param name="insertedOn"></param>
    /// <param name="updatedOn"></param>
    /// <param name="insertedBy"></param>
    /// <param name="updatedBy"></param>
    public Pessoa(string departamentoId, string pessoaId, string cpf, string preNome, string nomeDoMeio, string sobreNome, EStatus status, DateTime insertedOn, DateTime updatedOn, string insertedBy, string updatedBy)
        : base(departamentoId, pessoaId, cpf, preNome, nomeDoMeio, sobreNome, status, insertedOn, updatedOn, insertedBy, updatedBy)
    {
    }
}

public sealed class Usuario : Domain.Entities.Usuario
{
    [BsonId] public ObjectId _id { get; set; }

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="pessoaId"></param>
    /// <param name="name"></param>
    /// <param name="perfilNumero"></param>
    /// <param name="nivelNumero"></param>
    /// <param name="email"></param>
    /// <param name="senha"></param>
    /// <param name="numeroMaximoDeSessoesAtivas"></param>
    /// <param name="insertedBy"></param>
    public Usuario(string pessoaId, string name, short perfilNumero, short nivelNumero, string email, string senha, long numeroMaximoDeSessoesAtivas, string insertedBy)
        : base(pessoaId, name, perfilNumero, nivelNumero, email, senha, numeroMaximoDeSessoesAtivas, insertedBy)
    {        
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pessoaId"></param>
    /// <param name="name"></param>
    /// <param name="perfilNumero"></param>
    /// <param name="nivelNumero"></param>
    /// <param name="email"></param>
    /// <param name="senha"></param>
    /// <param name="sessoesAtivas"></param>
    /// <param name="numeroMaximoDeSessoesAtivas"></param>
    /// <param name="status"></param>
    /// <param name="insertedOn"></param>
    /// <param name="insertedBy"></param>
    /// <param name="updatedBy"></param>
    public Usuario(string id, string pessoaId, string name, short perfilNumero, short nivelNumero, string email, string senha, long sessoesAtivas, long numeroMaximoDeSessoesAtivas, EStatus status, DateTime insertedOn, string insertedBy, string updatedBy)
        : base(id, pessoaId, name, perfilNumero, nivelNumero, email, senha, sessoesAtivas, numeroMaximoDeSessoesAtivas, status, insertedOn, insertedBy, updatedBy)
    {
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pessoaId"></param>
    /// <param name="perfilNumero"></param>
    /// <param name="nivelNumero"></param>
    /// <param name="email"></param>
    /// <param name="senha"></param>
    /// <param name="sessoesAtivas"></param>
    /// <param name="numeroMaximoDeSessoesAtivas"></param>
    /// <param name="name"></param>
    /// <param name="status"></param>
    /// <param name="insertedOn"></param>
    /// <param name="updatedOn"></param>
    /// <param name="insertedBy"></param>
    /// <param name="updatedBy"></param>
    public Usuario(string id, string pessoaId, short perfilNumero, short nivelNumero, string email,
                   string senha, long sessoesAtivas, long numeroMaximoDeSessoesAtivas, string name,
                   EStatus status, DateTime insertedOn, DateTime updatedOn, string insertedBy, string updatedBy)
        : base(id, pessoaId, perfilNumero, nivelNumero, email, senha, sessoesAtivas, numeroMaximoDeSessoesAtivas, name, status, insertedOn, updatedOn, insertedBy, updatedBy)
    {
    }
}