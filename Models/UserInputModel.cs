using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MorseCodeServer.Models
{
    public class UserInputModel 
    {
        public string Input { get; set; }
        public int Frequency { get; set; } = 600;
    }
}
