using Domain.Entities;
using NetCore.Base.Enum;

namespace Domain.Builders
{
    public class EmpresaBuilder
    {
        private string _id;
        private EStatus _status;
        private DateTime _insertedOn;
        private string _insertedBy;
        private string _updatedBy;
        private DateTime _updatedOn;

        private string _grupoId;
        private string _cnpj;
        private string _inscricaoEstadual;
        private string _nomeFantasia;
        private string _razaoSocial;
        private string _cpfDoAdministrador;
        private string _preNomeDoAdministrador;
        private string _nomeDoMeioDoAdministrador;
        private string _sobreNomeDoAdministrador;
        private string _emailDoAdministrador;
        
        public EmpresaBuilder ComId(string id)
        {
            _id = id;
            return this;
        }

        public EmpresaBuilder ComStatus(EStatus status)
        {
            _status = status;
            return this;
        }

        public EmpresaBuilder ComInsertedOn(DateTime insertedOn)
        {
            _insertedOn = insertedOn;
            return this;
        }

        public EmpresaBuilder ComUpdatedOn(DateTime updatedOn)
        {
            _updatedOn = updatedOn;
            return this;
        }

        public EmpresaBuilder ComCpfDoAdministrador(string cpfDoAdministrador)
        {
            _cpfDoAdministrador = cpfDoAdministrador;
            return this;
        }

        public EmpresaBuilder ComPreNomeDoAdministrador(string preNomeDoAdministrador)
        {
            _preNomeDoAdministrador = preNomeDoAdministrador;
            return this;
        }

        public EmpresaBuilder ComNomeDoMeioDoAdministrador(string nomeDoMeioDoAdministrador)
        {
            _nomeDoMeioDoAdministrador = nomeDoMeioDoAdministrador;
            return this;
        }

        public EmpresaBuilder ComSobreNomeDoAdministrador(string sobreNomeDoAdministrador)
        {
            _sobreNomeDoAdministrador = sobreNomeDoAdministrador;
            return this;
        }

        public EmpresaBuilder ComEmailDoAdministrador(string emailDoAdministrador)
        {
            _emailDoAdministrador = emailDoAdministrador;
            return this;
        }

        public EmpresaBuilder ComGrupoId(string grupoId)
        {
            _grupoId = grupoId;
            return this;
        }

        public EmpresaBuilder ComCnpj(string cnpj)
        {
            _cnpj = cnpj;
            return this;
        }

        public EmpresaBuilder ComInscricaoEstadual(string inscricaoEstadual)
        {
            _inscricaoEstadual = inscricaoEstadual;
            return this;
        }

        public EmpresaBuilder ComNomeFantasia(string nomeFantasia)
        {
            _nomeFantasia = nomeFantasia;
            return this;
        }

        public EmpresaBuilder ComRazaoSocial(string razaoSocial)
        {
            _razaoSocial = razaoSocial;
            return this;
        }

        public EmpresaBuilder ComInsertedBy(string insertedBy)
        {
            _insertedBy = insertedBy;
            return this;
        }

        public Empresa BuildForCreate()
        {
            return new Empresa(_grupoId, _cnpj, _inscricaoEstadual, _nomeFantasia, _razaoSocial,
            _cpfDoAdministrador, _preNomeDoAdministrador, _nomeDoMeioDoAdministrador, _sobreNomeDoAdministrador, _emailDoAdministrador, _insertedBy);
        }
        public Empresa BuildForGet()
        {
            return new Empresa(_grupoId, _id, _cnpj, _inscricaoEstadual, _nomeFantasia, _razaoSocial, _insertedBy, _insertedOn, _updatedBy, _updatedOn);
        }
    }
}
