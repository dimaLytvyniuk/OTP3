using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4.BLL.DTO
{
    public class UserRegistrationDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Admin { get; set; }
        public string Password { get; set; }
    }
}
