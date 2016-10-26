using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class RequestTokenDto
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string UserID { get; set; }
    }
}
