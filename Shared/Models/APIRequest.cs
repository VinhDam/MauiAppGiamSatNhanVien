using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static Shared.Utilities.SD;

namespace Shared.Models
{
    public class APIRequest
    {
        public ApiType apiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
