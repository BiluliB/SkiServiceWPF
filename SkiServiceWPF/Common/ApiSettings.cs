using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceWPF.Common
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; }
        public string LoginEndpoint { get; set; }
        public string UserCreationEndpoint { get; set; }
        public string ServiceRequestsEndpoint { get; set; }
        // Weitere Eigenschaften nach Bedarf
    }
}
