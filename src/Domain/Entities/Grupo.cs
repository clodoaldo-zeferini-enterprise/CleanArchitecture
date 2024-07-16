using NetCore.Base.Enum;
using Domain.Validation;
using NetCore.Base;
using Domain.Builders;
using System.Xml.Linq;

namespace Domain.Entities
{
     public sealed class Grupo : Root
    {
        public string Id { get; private set; }
        public ICollection<Empresa>? Empresas { get; private set; }

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
        public Grupo(  string nomeDoGrupo, string razaoSocial, string nomeFantasia
                     , string cnpj, string inscricaoEstadual
                     , string cpfDoAdministrador, string preNomeDoAdministrador, string nomeDoMeioDoAdministrador, string sobreNomeDoAdministrador, string emailDoAdministrador
                     , string insertedBy) : base(nomeDoGrupo,insertedBy)
        {
            Id = Guid.NewGuid().ToString();

            ValidateGrupoInsert();

            var empresaBuilder = new EmpresaBuilder()                
                .ComGrupoId(Id)
                .ComCnpj(cnpj)
                .ComInscricaoEstadual(inscricaoEstadual)
                .ComNomeFantasia(nomeFantasia)
                .ComRazaoSocial(razaoSocial)
                .ComCpfDoAdministrador(cpfDoAdministrador)
                .ComPreNomeDoAdministrador(preNomeDoAdministrador)
                .ComNomeDoMeioDoAdministrador(nomeDoMeioDoAdministrador)
                .ComSobreNomeDoAdministrador(sobreNomeDoAdministrador)
                .ComEmailDoAdministrador(emailDoAdministrador)
                .ComInsertedBy(insertedBy);

            Domain.Entities.Empresa empresa = empresaBuilder.BuildForCreate();
      
            AddEmpresa(empresa);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nomeDoGrupo"></param>
        /// <param name="status"></param>
        /// <param name="insertedOn"></param>
        /// <param name="insertedBy"></param>
        /// <param name="updatedOn"></param>
        /// <param name="updatedBy"></param>
        /// <param name="empresas"></param>
        public Grupo(string id, string nomeDoGrupo, EStatus status, DateTime insertedOn, string insertedBy, DateTime updatedOn, string updatedBy, ICollection<Empresa> empresas) 
            : base(nomeDoGrupo, status, insertedOn, updatedOn, insertedBy, updatedBy)
        {
            Id = id;
            Empresas = empresas;
        }

        public void Update(NetCore.Base.Enum.EStatus status, string updatedBy)
        {
            base.SetStatus(status, updatedBy);
        }
        public void Delete(string updatedBy)
        {
            base.SetStatus(EStatus.EXCLUIDO, updatedBy);
        }

        private void ValidateGrupoInsert()
        {
            ValidadorDeRegra
                .Novo()
                .DispararExcecaoSeExistir();
        }

        private void ValidateGrupoUpdate()
        {
            ValidadorDeRegra
                .Novo()
                .Quando(!ValidateGuid(UpdatedBy), "UpdatedBy is required")
                .DispararExcecaoSeExistir();
        }

        public void AddEmpresa(Empresa empresa)
        {
            Empresas ??= new List<Empresa>();
            Empresas.Add(empresa);
        }
    }

     public sealed class Empresa : NetCore.Base.PessoaJuridica
    {
        public string Id { get; private set; }
        public string GrupoId { get; private set; }
        public string NomeFantasia { get; private set; }
        public string DepartamentoSysMegaId { get; private set; }
        public string PessoaAdministradorId { get; private set; }
        public string CPFDoAdministrador { get; private set; }
        public string NomeDoAdministrador { get; private set; }
        public string EmailDoAdministrador { get; private set; }
        public string PessoaUsuarioId { get; private set; }
        public ICollection<Filial> Filiais { get; private set; }
        public ICollection<Sistema> Sistemas { get; private set; }





        /// <summary>
        /// Insert
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
            : base(cnpj, inscricaoEstadual, nomeFantasia, razaoSocial, insertedBy)
        {
            GrupoId = grupoId;
            Id = Guid.NewGuid().ToString();
            NomeFantasia = nomeFantasia;
            DepartamentoSysMegaId = Guid.NewGuid().ToString();
            PessoaAdministradorId = Guid.NewGuid().ToString();
            CPFDoAdministrador = cpfDoAdministrador;
            NomeDoAdministrador = $@"{preNomeDoAdministrador} {nomeDoMeioDoAdministrador} {sobreNomeDoAdministrador}";
            EmailDoAdministrador = emailDoAdministrador;
            PessoaUsuarioId = Guid.NewGuid().ToString();

            ValidateEmpresaInsert();

            Filial filial = new Filial(Id, CNPJ, InscricaoEstadual, NomeFantasia, RazaoSocial, insertedBy);
            Departamento departamento = new Departamento(filial.Id, DepartamentoSysMegaId, "Administradores", insertedBy);
            Pessoa pessoa = new Pessoa(departamento.Id, PessoaAdministradorId, CPFDoAdministrador, preNomeDoAdministrador, nomeDoMeioDoAdministrador, sobreNomeDoAdministrador, insertedBy);
            Usuario usuario = new Usuario(pessoa.Id, NomeDoAdministrador, 1, 1, emailDoAdministrador, "123mudar", 10, insertedBy);

            pessoa.AddUsuario(usuario);
            departamento.AddPessoa(pessoa);
            filial.AddDepartamento(departamento);
            AddFilial(filial);
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
            : base(cnpj, inscricaoEstadual, nomeFantasia, razaoSocial, status, insertedOn, insertedBy, updatedBy)
        {
            GrupoId = grupoId;
            Id = id;

            ValidateEmpresaUpdate();
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
            : base(cnpj, inscricaoEstadual, nomeFantasia, razaoSocial, insertedBy)
        {
            Id = id;
            GrupoId = grupoId;
        }

        private void ValidateEmpresaInsert()
        {
            NetCore.Base.ValidadorDeRegra
                .Novo()
                .Quando(!ValidateGuid(Id), "Id is required")
                .Quando(!ValidateGuid(GrupoId), "GrupoId is required")
                .Quando(!ValidateGuid(DepartamentoSysMegaId), "DepartamentoSysMegaId is required")
                .Quando(!ValidateGuid(PessoaAdministradorId), "PessoaAdministradorId is required")
                .Quando(!ValidateGuid(PessoaUsuarioId), "PessoaUsuarioId is required")
                .Quando(string.IsNullOrEmpty(NomeDoAdministrador), "NomeDoAdministrador is required")
                .Quando(string.IsNullOrEmpty(EmailDoAdministrador), "EmailDoAdministrador is required")

                .Quando(string.IsNullOrEmpty(InscricaoEstadual), "InscricaoEstadual is required")
                .Quando(string.IsNullOrEmpty(RazaoSocial), "RazaoSocial is required")
                .Quando(string.IsNullOrEmpty(NomeFantasia), "NomeFantasia is required")
                .DispararExcecaoSeExistir();
        }

        private void ValidateEmpresaUpdate()
        {
            NetCore.Base.ValidadorDeRegra
                .Novo()
                .Quando(GrupoId == string.Empty.ToString(), "GrupoId is required")
                .Quando(Id == string.Empty.ToString(), "Id is required")
                .DispararExcecaoSeExistir();
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

     public sealed class Filial : NetCore.Base.PessoaJuridica
    {
        public string EmpresaId { get; private set; }
        public string Id { get; private set; }
        public EStatus Status { get; private set; }
        public string CNPJ { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeDaFilial { get; private set; }
        public ICollection<Departamento> Departamentos { get; private set; }

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
            : base(cnpj, inscricaoEstadual, nome, razaoSocial, insertedBy)
        {
            EmpresaId = empresaId;
            CNPJ = cnpj;
            InscricaoEstadual = inscricaoEstadual;
            RazaoSocial = razaoSocial;
            Status = EStatus.ATIVO;
            Id = Guid.NewGuid().ToString();
            NomeDaFilial = nome;

            ValidateFilialInsert();
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
            : base(cnpj, inscricaoEstadual, nome, razaoSocial, status, insertedOn, insertedBy, updatedBy)
        {
            EmpresaId = empresaId;
            Id = id;

            ValidateFilialUpdate();
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
            : base(cnpj, inscricaoEstadual, nomeFantasia, razaoSocial, status, insertedOn, insertedBy, updatedBy)
        {
            EmpresaId = empresaId;
            Id = id;

            ValidateFilialUpdate();
        }

        private void ValidateFilialInsert()
        {
            NetCore.Base.ValidadorDeRegra
                .Novo()
                .Quando(!ValidateGuid(EmpresaId), "EmpresaId is required")
                .Quando(!ValidateGuid(Id), "Id is required")
                .Quando(string.IsNullOrEmpty(CNPJ), "CNPJ is required")
                .Quando(string.IsNullOrEmpty(InscricaoEstadual), "InscricaoEstadual is required")
                .Quando(string.IsNullOrEmpty(RazaoSocial), "RazaoSocial is required")
                .Quando(string.IsNullOrEmpty(NomeDaFilial), "NomeDaFilial is required")

                .DispararExcecaoSeExistir();
        }

        private void ValidateFilialUpdate()
        {
            NetCore.Base.ValidadorDeRegra
                .Novo()
                .Quando(EmpresaId == string.Empty.ToString(), "EmpresaId is required")
                .Quando(Id == string.Empty.ToString(), "Id is required")
                .Quando(InsertedOn == DateTime.MinValue, "InsertedOn is required")
                .Quando(InsertedOn > UpdatedOn, "InsertedOn is greater than UpdatedOn")
                .Quando(UpdatedBy == string.Empty.ToString(), "UpdatedBy is required")
                .DispararExcecaoSeExistir();
        }

        public void AddDepartamento(Departamento departamento)
        {
            Departamentos ??= new List<Departamento>();
            Departamentos.Add(departamento);
        }
    }

     public sealed class Departamento : Root
    {
        public string FilialId { get; private set; }
        public string Id { get; private set; }
        public ICollection<Pessoa>? Pessoas { get; private set; }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="filialId"></param>
        /// <param name="departamentoId"></param>
        /// <param name="name"></param>
        /// <param name="insertedBy"></param>
        public Departamento(string filialId, string departamentoId, string name, string insertedBy) : base(name, insertedBy)
        {
            FilialId = filialId;
            Id = departamentoId; 

            ValidateDepartamentoInsert();
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
            base(name, status, insertedOn, insertedBy, updatedBy)
        {
            FilialId = filialId;
            Id = departamentoId;

            ValidateDepartamentoUpdate();
        }

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
            base(name, status, insertedOn, updatedOn, insertedBy, updatedBy)
        {
            FilialId = filialId;
            Id = departamentoId;
        }

        private void ValidateDepartamentoInsert()
        {
            NetCore.Base.ValidadorDeRegra
                .Novo()
                .Quando(!ValidateGuid(FilialId), "FilialId is required")
                .Quando(!ValidateGuid(Id), "Id is required")
                .DispararExcecaoSeExistir();
        }

        private void ValidateDepartamentoUpdate()
        {
            NetCore.Base.ValidadorDeRegra
                .Novo()
                .Quando(FilialId == string.Empty.ToString(), "FilialId is required")
                .Quando(Id == string.Empty.ToString(), "Id is required")
                .DispararExcecaoSeExistir();
        }

        public void AddPessoa(Pessoa pessoa)
        {
            NetCore.Base.ValidadorDeRegra
            .Novo()
            .Quando((pessoa == null), "Id is required")
            .DispararExcecaoSeExistir();

            Pessoas ??= new List<Pessoa>();
            Pessoas.Add(pessoa);
        }
    }

     public sealed class Pessoa : PessoaFisica
    {
        public string DepartamentoId { get; private set; }

        public ICollection<Usuario>? Usuarios { get; private set; }

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
            : base(pessoaId, cpf, preNome, nomeDoMeio, sobreNome, insertedBy)
        {
            DepartamentoId = departamentoId;
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
            : base(pessoaId, cpf, preNome, nomeDoMeio, sobreNome, status, insertedOn, insertedBy, updatedBy)
        {
            DepartamentoId = departamentoId;
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
            : base(pessoaId, cpf, preNome, nomeDoMeio, sobreNome, status, insertedOn, updatedOn, insertedBy, updatedBy)
        {
            DepartamentoId = departamentoId;
        }

        public void AddUsuario(Usuario usuario)
        {
            NetCore.Base.ValidadorDeRegra
            .Novo()
            .Quando((usuario == null), "usuario is required")
            .DispararExcecaoSeExistir();

            Usuarios ??= new List<Usuario>();
            Usuarios.Add(usuario);
        }

        public void AddUsuario(ICollection<Usuario> usuarios)
        {
            NetCore.Base.ValidadorDeRegra
            .Novo()
            .Quando((usuarios == null), "usuarios is required")
            .DispararExcecaoSeExistir();

            Usuarios ??= new List<Usuario>();
            Usuarios = usuarios;
        }
    }

     public sealed class Usuario : Root
    {
        public string PessoaId { get; private set; }
        public string Id { get; private set; }
        public short PerfilNumero { get; private set; }
        public short NivelNumero { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public long SessoesAtivas { get; private set; }
        public long NumeroMaximoDeSessoesAtivas { get; private set; }
        public ICollection<Direito>? Direitos { get; private set; }


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
            : base(name, insertedBy)
        {
            Id = Guid.NewGuid().ToString();
            PessoaId = pessoaId;
            PerfilNumero = perfilNumero;
            NivelNumero = nivelNumero;
            Email = email;
            Senha = senha;
            NumeroMaximoDeSessoesAtivas = numeroMaximoDeSessoesAtivas;

            ValidateUsuarioInsert();
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
            : base(name, status, insertedOn, insertedBy, updatedBy)
        {
            Id = id;
            PessoaId = pessoaId;
            PerfilNumero = perfilNumero;
            NivelNumero = nivelNumero;
            Email = email;
            Senha = senha;
            SessoesAtivas = sessoesAtivas;
            NumeroMaximoDeSessoesAtivas = numeroMaximoDeSessoesAtivas;

            ValidateUsuarioUpdate();
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
            : base(name, status, insertedOn, updatedOn, insertedBy, updatedBy)
        {
            Id = id;
            PessoaId = pessoaId;
            PerfilNumero = perfilNumero;
            NivelNumero = nivelNumero;
            Email = email;
            Senha = senha;
            SessoesAtivas = sessoesAtivas;
            NumeroMaximoDeSessoesAtivas = numeroMaximoDeSessoesAtivas;
        }


        private void ValidateUsuarioInsert()
        {
            NetCore.Base.ValidadorDeRegra
                .Novo()
                .Quando(PessoaId == string.Empty.ToString(), "PessoaId is required")
                .Quando(string.IsNullOrEmpty(Name), "Name is required")
                .Quando(PerfilNumero == 0, "PerfilNumero is required")
                .Quando(NivelNumero == 0, "NivelNumero is required")
                .Quando(string.IsNullOrEmpty(Email), "Email is required")
                .Quando(string.IsNullOrEmpty(Senha), "Senha is required")
                .Quando(NumeroMaximoDeSessoesAtivas == 0, "NumeroMaximoDeSessoesAtivas is required")
                .Quando(InsertedBy == string.Empty.ToString(), "InsertedBy is required")
                .DispararExcecaoSeExistir();
        }

        private void ValidateUsuarioUpdate()
        {
            NetCore.Base.ValidadorDeRegra
                .Novo()
                .Quando(!ValidateGuid(Id), "Id is required")
                .Quando(PessoaId == string.Empty.ToString(), "PessoaId is required")
                .Quando(string.IsNullOrEmpty(Name), "Name is required")
                .Quando(PerfilNumero == 0, "PerfilNumero is required")
                .Quando(NivelNumero == 0, "NivelNumero is required")
                .Quando(string.IsNullOrEmpty(Email), "Email is required")
                .Quando(string.IsNullOrEmpty(Senha), "Senha is required")
                .Quando(NumeroMaximoDeSessoesAtivas == 0, "NumeroMaximoDeSessoesAtivas is required")
                .Quando(InsertedOn == DateTime.MinValue, "InsertedOn is required")
                .Quando(InsertedOn > UpdatedOn, "InsertedOn is greater than UpdatedOn")
                .Quando(UpdatedBy == string.Empty.ToString(), "UpdatedBy is required")
                .DispararExcecaoSeExistir();
        }

        public void AddDireito(Direito direito)
        {
            NetCore.Base.ValidadorDeRegra
            .Novo()
            .Quando((direito == null), "Direito is required")
            .DispararExcecaoSeExistir();

            Direitos ??= new List<Direito>();
            Direitos.Add(direito);
        }
    }


}
