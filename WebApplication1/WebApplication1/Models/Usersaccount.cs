using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Usersaccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
