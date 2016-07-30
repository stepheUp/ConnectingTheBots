using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IAssistRequestService
    {
        [OperationContract]
        Task<int> CreateAssistRequest(string firstmessage);

        [OperationContract]
        Task AddMessage(int AssistId, string msg);

        [OperationContract]
        Task<string> GetLastMessage(int AssistIdd);

        [OperationContract]
        Task<List<string>> GetAllMessages(int AssistId);


    }
}
