using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Base.Configurations
{
    public class SwaggerConfig
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public SwaggerContact Contact { get; set; }
        public SwaggerLicense License { get; set; }
    }
}
