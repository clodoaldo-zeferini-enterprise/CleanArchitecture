using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Base.Configurations
{
    public class SqlServerSettings
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool MultipleActiveResultSets { get; set; }
        public bool Encrypt { get; set; }
        public bool TrustServerCertificate { get; set; }
        public int ConnectionTimeout { get; set; }
    }
}
