using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MorseCodeServer.Services
{
    public class RequestHandler : IRequestHandler
    {
        private MorseDecoder _decoder = new MorseDecoder();

        public Task<StringBuilder> Handle(string msg)
        {
            var decodedInput = Task.FromResult(_decoder.Decode(msg));
            return decodedInput;

        }
    }
}
