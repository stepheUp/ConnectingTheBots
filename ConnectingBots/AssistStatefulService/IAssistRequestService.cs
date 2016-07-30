using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace AssistStatefulService
{
    
    public interface IAssistRequestService : IService
    {
         Task<int> CreateAssistRequest(string firstmessage);
        
        Task AddMessage(int AssistId, string msg);

        Task<string> GetLastMessage(int AssistIdd);
       
        Task<List<string>> GetAllMessages(int AssistId);
    }
}
