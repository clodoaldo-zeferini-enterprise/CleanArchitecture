using Domain.Entities;
using NetCore.Base;
using NetCore.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Builders
{
    public class PessoaBuilder
    {
        private string _id;
        private EStatus _status;
        private DateTime _insertedOn;
        private string _insertedBy;
        private string _updatedBy;
        private DateTime _updatedOn;


        private string _departamentoId;

        private string _cpf;
        private string _preNome;
        private string _nomeDoMeio;
        private string _sobreNome;

        private ICollection<Usuario> _usuarios;


        public PessoaBuilder ComId(string id)
        {
            _id = id;
            return this;
        }

        public PessoaBuilder ComStatus(EStatus status)
        {
            _status = status;
            return this;
        }

        public PessoaBuilder ComInsertedOn(DateTime insertedOn)
        {
            _insertedOn = insertedOn;
            return this;
        }

        public PessoaBuilder ComUpdatedOn(DateTime updatedOn)
        {
            _updatedOn = updatedOn;
            return this;
        }

        public PessoaBuilder ComUpdatedBy(string updatedBy)
        {
            _updatedBy = updatedBy;
            return this;
        }



        public PessoaBuilder ComDepartamentoId(string departamentoId)
        {
            _departamentoId = departamentoId;
            return this;
        }

        public PessoaBuilder ComCpf(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public PessoaBuilder ComPreNome(string preNome)
        {
            _preNome = preNome;
            return this;
        }

        public PessoaBuilder ComNomeDoMeio(string nomeDoMeio)
        {
            _nomeDoMeio = nomeDoMeio;
            return this;
        }

        public PessoaBuilder ComSobreNome(string sobreNome)
        {
            _sobreNome = sobreNome;
            return this;
        }

        public PessoaBuilder ComInsertedBy(string insertedBy)
        {
            _insertedBy = insertedBy;
            return this;
        }

        public PessoaBuilder ComUsuarios(ICollection<Usuario> usuarios)
        {
            _usuarios = usuarios;
            return this;
        }

        public Domain.Entities.Pessoa BuildForCreate()
        {
            var pessoa =  new Domain.Entities.Pessoa(_departamentoId, _id, _cpf, _preNome, _nomeDoMeio, _sobreNome, _insertedBy);

            pessoa.AddUsuario(_usuarios);

            return pessoa;
        }

        public Domain.Entities.Pessoa BuildForGet()
        {
            var pessoa = new Domain.Entities.Pessoa(_departamentoId, _id, _cpf, _preNome, _nomeDoMeio, _sobreNome, _status, _insertedOn, _updatedOn, _insertedBy, _updatedBy);

            pessoa.AddUsuario(_usuarios);

            return pessoa;
        }
    }
}
