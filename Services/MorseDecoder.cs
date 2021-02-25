using MorseCodeServer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseCodeServer.Services
{
    public class MorseDecoder : IMorseDecoder
    {
        public StringBuilder Decode(string input)
        {
            StringBuilder decodedString = new StringBuilder();
            foreach (var letter in input)
            {
                var decoded = MorseCodeLib.Morse[letter];
                decodedString.Append(decoded);
                decodedString.Append(" ");
            }
            return decodedString;
        }
    }
}
