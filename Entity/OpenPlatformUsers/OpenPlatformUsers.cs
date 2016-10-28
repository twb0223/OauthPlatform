using System;
using System.Collections.Generic;

namespace Entity
{

    public partial class OpenPlatformUsers
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
