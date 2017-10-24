using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theworldcore.Services
{
    public interface IMessageSender
    {
        void SendMessage(string from, string to, string message);
    }
}
