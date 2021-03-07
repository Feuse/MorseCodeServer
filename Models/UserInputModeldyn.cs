using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MorseCodeServer.Models
{
    public class UserInputModeldyn
    {
        public string morse { get; set; }
        public  int Frequency { get; set; } = 600;
        public IFormFile file { get; set; }
        public string message { get; set; }
        public string Error { get; internal set; }
    }
}
