using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum EStatusCommand
    {
        CREATED_WITH_SUCCESS = 0,
        FAILED_TO_CREATE     = 1,

        UPDATED_WITH_SUCCESS = 2,
        FAILED_TO_UPDATE = 3,

        DELETED_WITH_SUCCESS = 4,
        FAILED_TO_DELETE = 5,
    }
}
