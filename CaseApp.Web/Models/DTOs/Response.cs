using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseApp.Web.Models.DTOs
{
    public class Response
    {
        public Content contents { get; set; }
        public Success success { get; set; }
    }
}
