using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
