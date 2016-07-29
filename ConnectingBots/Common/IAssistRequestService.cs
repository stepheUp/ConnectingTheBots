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
        Task<int> CreateAssistRequest();

        [OperationContract]
        Task AddMessage(string msg);

        [OperationContract]
        Task<string> GetLastMessage();

        [OperationContract]
        Task<List<string>> GetAllMessages();


    }
}
