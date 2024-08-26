using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Base.Configurations
{
    public class DynamoDBConfig
    {
        public string ServiceURL { get; set; }
        public bool UseLocalMode { get; set; }
        public string AWSAccessKey { get; set; }
        public string AWSSecretKey { get; set; }
        public string Region { get; set; }
    }
}
