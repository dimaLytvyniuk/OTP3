using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4.BLL.DTO
{
    public class UserProfileDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Admin { get; set; }
    }
}
