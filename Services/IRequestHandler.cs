using System.Text;
using System.Threading.Tasks;

namespace MorseCodeServer.Services
{
    public interface IRequestHandler
    {
        public Task<string> Handle(string msg);
    }
}