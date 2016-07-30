using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System.ServiceModel;
using System.Diagnostics;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Runtime;
using System.ServiceModel.Channels;
using System.Web.Services.Description;
using Microsoft.ServiceFabric.Services.Communication.Wcf;

namespace AssistStatefulService
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class AssistStatefulService : StatefulService, IAssistRequestService
    {
        public AssistStatefulService(StatefulServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see http://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {  
                new ServiceReplicaListener(context =>
                        new WcfCommunicationListener<IAssistRequestService>(
                            wcfServiceObject:this,
                            serviceContext:context,
                            endpointResourceName:"ServiceEndpoint",
                            listenerBinding:this.CreateListenBinding()
                            //listenerBinding: WcfUtility.CreateTcpListenerBinding()
                        ))
                     /*   , 
                new ServiceReplicaListener(context =>
                        new WcfCommunicationListener<IAssistRequestService>(
                            wcfServiceObject:this,
                            serviceContext:context,
                            endpointResourceName:"WcfServiceEndpoint",
                            listenerBinding:this.CreateHttpListenBinding())
                        ) */
                        

             };
        }

        private NetHttpBinding CreateHttpListenBinding()
        {

            NetHttpBinding binding = new NetHttpBinding(BasicHttpSecurityMode.None)
            {
                SendTimeout = TimeSpan.MaxValue,
                ReceiveTimeout = TimeSpan.MaxValue,
                OpenTimeout = TimeSpan.FromSeconds(5),
                CloseTimeout = TimeSpan.FromSeconds(5),
                MaxReceivedMessageSize = 1024 * 1024
            };

            binding.MaxBufferSize = (int)binding.MaxReceivedMessageSize;
            binding.MaxBufferPoolSize = Environment.ProcessorCount * binding.MaxReceivedMessageSize;

            return binding;
        }


        private NetTcpBinding CreateListenBinding()
        {
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None)
            {
                SendTimeout = TimeSpan.MaxValue,
                ReceiveTimeout = TimeSpan.MaxValue,
                OpenTimeout = TimeSpan.FromSeconds(5),
                CloseTimeout = TimeSpan.FromSeconds(5),
                MaxConnections = int.MaxValue,
                MaxReceivedMessageSize = 1024 * 1024
            };

            binding.MaxBufferSize = (int)binding.MaxReceivedMessageSize;
            binding.MaxBufferPoolSize = Environment.ProcessorCount * binding.MaxReceivedMessageSize;

            return binding;

        }
        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

              while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

        public async Task<int> CreateAssistRequest(string firstmessage)
        {
            var assistRequest = new AssistRequestItem();
            Random rnd = new Random();
            assistRequest.IdAssistItem =  rnd.Next();
 
            var assistRequestCol = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, AssistRequestItem>>("myAssistRequests");

            using (var tx = this.StateManager.CreateTransaction())
            {

                await assistRequestCol.AddOrUpdateAsync(tx, assistRequest.IdAssistItem.ToString(), assistRequest, (k,v) => assistRequest);
                await tx.CommitAsync();
            }

                return assistRequest.IdAssistItem;
        }

        public async Task AddMessage(int AssistId, string msg)
        {
            AssistRequestItem item;
            var assistRequestCol = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, AssistRequestItem>>("myAssistRequests");
            using (var tx = this.StateManager.CreateTransaction())
            {
                var result =  await assistRequestCol.TryGetValueAsync(tx, AssistId.ToString());
                if (result.HasValue)
                {
                    item = result.Value;
                    item.Messages.Add(msg);
                    await tx.CommitAsync();
                }
            }

        }

        public Task<string> GetLastMessage(int AssistId)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllMessages(int AssistId)
        {
            throw new NotImplementedException();
        }
    }
}
