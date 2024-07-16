using Domain.Entities;
using NetCore.Base.Enum;

namespace Domain.Builders
{
    public class FilialBuilder
    {
        private string _id;
        private EStatus _status;
        private DateTime _insertedOn;
        private string _insertedBy;
        private string _updatedBy;
        private DateTime _updatedOn;

        private string _empresaId;

        private string _cnpj;
        private string _inscricaoEstadual;
        private string _razaoSocial;
        private string _nomeDaFilial;
            


        private ICollection<Departamento> _departamentos;


        public FilialBuilder ComInsertedOn(DateTime insertedOn)
        {
            _insertedOn = insertedOn;
            return this;
        }

        public FilialBuilder ComUpdatedOn(DateTime updatedOn)
        {
            _updatedOn = updatedOn;
            return this;
        }


        public FilialBuilder ComEmpresaId(string empresaId)
        {
            _empresaId = empresaId;
            return this;
        }

        public FilialBuilder ComId(string id)
        {
            _id = id;
            return this;
        }

        public FilialBuilder ComStatus(EStatus status)
        {
            _status = status;
            return this;
        }

        public FilialBuilder ComCnpj(string cnpj)
        {
            _cnpj = cnpj;
            return this;
        }

        public FilialBuilder ComInscricaoEstadual(string inscricaoEstadual)
        {
            _inscricaoEstadual = inscricaoEstadual;
            return this;
        }

        public FilialBuilder ComRazaoSocial(string razaoSocial)
        {
            _razaoSocial = razaoSocial;
            return this;
        }

        public FilialBuilder ComNomeDaFilial(string nomeDaFilial)
        {
            _nomeDaFilial = nomeDaFilial;
            return this;
        }

        public FilialBuilder ComDepartamentos(ICollection<Departamento> departamentos)
        {
            _departamentos = departamentos;
            return this;
        }

        public FilialBuilder ComInsertedBy(string insertedBy)
        {
            _insertedBy = insertedBy;
            return this;
        }

        public Filial BuildForCreate()
        {
            return new Filial(_empresaId, _cnpj, _inscricaoEstadual, _nomeDaFilial, _razaoSocial, _insertedBy);
        }

        public Filial BuildForGet()
        {
            return new Filial(_empresaId, _id, _cnpj, _inscricaoEstadual, _nomeDaFilial, _razaoSocial, _status, _insertedOn, _updatedOn, _insertedBy, _updatedBy);
        }
    }
}
