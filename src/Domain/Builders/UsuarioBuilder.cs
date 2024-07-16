using Domain.Entities;
using NetCore.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Builders
{
    public class UsuarioBuilder
    {
        private string  _pessoaId;  
        private string  _name; 
        private short  _perfilNumero;  
        private short  _nivelNumero;  
        private string  _email;  
        private string  _senha; 
        private short   _numeroMaximoDeSessoesAtivas;
        private string _insertedBy;


        public UsuarioBuilder ComPessoaId(string pessoaId)
        {
            _pessoaId = pessoaId;
            return this;
        }

        public UsuarioBuilder ComName(string name)
        {
            _name = name;
            return this;
        }

        public UsuarioBuilder ComPerfilNumero(short perfilNumero)
        {
            _perfilNumero = perfilNumero;
            return this;
        }

        public UsuarioBuilder ComNivelNumero(short nivelNumero)
        {
            _nivelNumero = nivelNumero;
            return this;
        }

        public UsuarioBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public UsuarioBuilder ComSenha(string senha)
        {
            _senha = senha;
            return this;
        }

        public UsuarioBuilder ComNumeroMaximoDeSessoesAtivas(short numeroMaximoDeSessoesAtivas)
        {
            _numeroMaximoDeSessoesAtivas = numeroMaximoDeSessoesAtivas;
            return this;
        }

        public UsuarioBuilder ComInsertedBy(string insertedBy)
        {
            _insertedBy = insertedBy;
            return this;
        }        

        public Usuario BuildForCreate()
        {
            var usuario =  new Usuario(_pessoaId, _name, _perfilNumero, _nivelNumero, _email, _senha, _numeroMaximoDeSessoesAtivas, _insertedBy);

            return usuario;
        }
    }
}
