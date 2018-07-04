using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactWeb.Libraries
{
    public class EmailInputDto
    {
        public string ToAddress { get; set; }
        public string ToFormalName { get; set; }
        public string FromAddress { get; set; }
        public string FromFormalName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
