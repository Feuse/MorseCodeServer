using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MorseCodeServer.Util
{
    public static class MorseCodeLib
    {
        public static Dictionary<char, string> Morse = new Dictionary<char, string>()
{
    {'A' , ".-"},
    {'B' , "-..."},
    {'C' , "-.-."},
    {'D' , "-.."},
    {'E' , "."},
    {'F' , "..-."},
    {'G' , "--."},
    {'H' , "...."},
    {'I' , ".."},
    {'J' , ".---"},
    {'K' , "-.-"},
    {'L' , ".-.."},
    {'M' , "--"},
    {'N' , "-."},
    {'O' , "---"},
    {'P' , ".--."},
    {'Q' , "--.-"},
    {'R' , ".-."},
    {'S' , "..."},
    {'T' , "-"},
    {'U' , "..-"},
    {'V' , "...-"},
    {'W' , ".--"},
    {'X' , "-..-"},
    {'Y' , "-.--"},
    {'Z' , "--.."},
    {'0' , "-----"},
    {'1' , ".----"},
    {'2' , "..---"},
    {'3' , "...--"},
    {'4' , "....-"},
    {'5' , "....."},
    {'6' , "-...."},
    {'7' , "--..."},
    {'8' , "---.."},
    {'9' , "----."},
};   
    }
}
