using System;
using System.Collections.Generic;

#nullable disable

namespace Backstage.Models
{
    public partial class Administer
    {
        public int AdministerId { get; set; }
        public string Account { get; set; }
        public string PasswordHash { get; set; }
    }
}
