

using System;

namespace MorseCodeServer.Services
{
    public interface IMorseDecoder
    {
        public string Decode(string input);
        public byte[] MorseBuilder(string msg);
    }
}