using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
    public abstract class RequestBase
    {
        public string UserId { get; set; }
        public string SysUsuSessionId { get; set; }
        private string RequestId { get; set; } = Guid.NewGuid().ToString();
        private DateTime RequestedIn { get; set; } = DateTime.Now;


        protected RequestBase()
        {
        }

        protected RequestBase(string userId, string sysUsuSessionId)
        {
            UserId = userId;
            SysUsuSessionId = sysUsuSessionId;
        }

    }
}
