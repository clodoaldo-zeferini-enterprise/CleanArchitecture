using Domain.Entities;
using NetCore.Base.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Builders
{
    public class GrupoBuilder
    {
        private string _id;
        private EStatus _status;
        private DateTime _insertedOn;
        private string _insertedBy;
        private string _updatedBy;
        private DateTime _updatedOn;

        private string  _nomeDoGrupo;
        private string  _razaoSocial;
        private string  _nomeFantasia;
        private string  _cnpj;
        private string  _inscricaoEstadual;
        private string  _cpfDoAdministrador;
        private string  _preNomeDoAdministrador;
        private string  _nomeDoMeioDoAdministrador;
        private string  _sobreNomeDoAdministrador;
        private string  _emailDoAdministrador;
        


        private List<Empresa> _empresas = new List<Empresa>();

        
        public GrupoBuilder ComInsertedOn(DateTime insertedOn)
        {
            _insertedOn = insertedOn;
            return this;
        }

        public GrupoBuilder ComUpdatedOn(DateTime updatedOn)
        {
            _updatedOn = updatedOn;
            return this;
        }



        public GrupoBuilder ComId(string id)
        {
            _id = id;
            return this;
        }

        public GrupoBuilder ComStatus(EStatus status)
        {
            _status = status;
            return this;
        }

        public GrupoBuilder ComNomeDoGrupo(string nomeDoGrupo)
        {
            _nomeDoGrupo = nomeDoGrupo;
            return this;
        }

        public GrupoBuilder ComRazaoSocial(string razaoSocial)
        {
            _razaoSocial = razaoSocial;
            return this;
        }

        public GrupoBuilder ComNomeFantasia(string nomeFantasia)
        {
            _nomeFantasia = nomeFantasia;
            return this;
        }

        public GrupoBuilder ComCnpj(string cnpj)
        {
            _cnpj = cnpj;
            return this;
        }

        public GrupoBuilder ComInscricaoEstadual(string inscricaoEstadual)
        {
            _inscricaoEstadual = inscricaoEstadual;
            return this;
        }

        public GrupoBuilder ComCpfDoAdministrador(string cpfDoAdministrador)
        {
            _cpfDoAdministrador = cpfDoAdministrador;
            return this;
        }

        public GrupoBuilder ComPreNomeDoAdministrador(string preNomeDoAdministrador)
        {
            _preNomeDoAdministrador = preNomeDoAdministrador;
            return this;
        }

        public GrupoBuilder ComNomeDoMeioDoAdministrador(string nomeDoMeioDoAdministrador)
        {
            _nomeDoMeioDoAdministrador = nomeDoMeioDoAdministrador;
            return this;
        }

        public GrupoBuilder ComSobreNomeDoAdministrador(string sobreNomeDoAdministrador)
        {
            _sobreNomeDoAdministrador = sobreNomeDoAdministrador;
            return this;
        }

        public GrupoBuilder ComEmailDoAdministrador(string emailDoAdministrador)
        {
            _emailDoAdministrador = emailDoAdministrador;
            return this;
        }

        public GrupoBuilder ComInsertedBy(string insertedBy)
        {
            _insertedBy = insertedBy;
            return this;
        }
        public GrupoBuilder ComUpdatedBy(string updatedBy)
        {
            _updatedBy = updatedBy;
            return this;
        }

        public GrupoBuilder AdicionarEmpresa(Empresa empresa)
        {
            _empresas.Add(empresa);
            return this;
        }

        public Grupo BuildForCreate()
        {
            var grupo = new Grupo(_nomeDoGrupo, _razaoSocial, _nomeFantasia, _cnpj, _inscricaoEstadual, _cpfDoAdministrador, _preNomeDoAdministrador, _nomeDoMeioDoAdministrador, _sobreNomeDoAdministrador, _emailDoAdministrador, _insertedBy);

            foreach (var empresa in _empresas)
            {
                grupo.AddEmpresa(empresa);
            }

            return grupo;
        }

        public Grupo BuildForGet()
        {
            var grupo = new Grupo(_id, _nomeDoGrupo, _razaoSocial, _nomeFantasia
                , _cnpj, _inscricaoEstadual, _cpfDoAdministrador, _preNomeDoAdministrador,
                _nomeDoMeioDoAdministrador, _sobreNomeDoAdministrador, _emailDoAdministrador,
                _status, _insertedOn, _insertedBy, _updatedOn, _updatedBy, _empresas);

            return grupo;
        }
    }
}
