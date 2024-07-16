using Domain.Entities;
using NetCore.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Builders
{
    public class DepartamentoBuilder
    {
        private string _id;
        private EStatus _status;
        private DateTime _insertedOn;
        private string _insertedBy;
        private string _updatedBy;
        private DateTime _updatedOn;


        private string _filialId;
        private string _name;

        private ICollection<Pessoa> _pessoas;


        public DepartamentoBuilder ComInsertedOn(DateTime insertedOn)
        {
            _insertedOn = insertedOn;
            return this;
        }

        public DepartamentoBuilder ComUpdatedOn(DateTime updatedOn)
        {
            _updatedOn = updatedOn;
            return this;
        }




        public DepartamentoBuilder ComFilialId(string filialId)
        {
            _filialId = filialId;
            return this;
        }

        public DepartamentoBuilder ComName(string name)
        {
            _id = name;
            return this;
        }


        public DepartamentoBuilder ComInsertedBy(string insertedBy)
        {
            _insertedBy = insertedBy;
            return this;
        }

        public DepartamentoBuilder ComUpdatedBy(string updatedBy)
        {
            _insertedBy = updatedBy;
            return this;
        }

        public Departamento BuildForCreate()
        {
            return new Departamento(_filialId, _id, _name, _insertedBy);
        }
    }
}
