using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class OpenPlatformUsersRegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class OpenPlatformUserLoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class OpenPlatformUserChangePasswordDto
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class OpenPlatformUsersRegisterOutputDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime CreateTime { get; set; }
    }

}
