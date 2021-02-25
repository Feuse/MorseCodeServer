using System.Text;

namespace MorseCodeServer.Services
{
    public interface IMorseDecoder
    {
        public StringBuilder Decode(string input);
    }
}