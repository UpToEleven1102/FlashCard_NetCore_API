using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCard.Models
{
    public class CustomIdentityUser:IdentityUser
    {
        public string First { get; set; }
        public string Last { get; set; }
    }
}
