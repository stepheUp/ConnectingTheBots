using KBStatelessService.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBStatelessService.Services
{
    public class FaqService
    {
        private string DatabaseId;
        private string CollectionId;
        private string EndPoint;
        private string AuthKey;

        private DocumentClient client;
        private DocumentCollection collection;

        private static readonly Lazy<FaqService> _instance =
            new Lazy<FaqService>(() => new FaqService());

        public static FaqService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private FaqService()
        {
            DatabaseId = ConfigurationManager.AppSettings.Get("database");
            CollectionId = ConfigurationManager.AppSettings.Get("collection");
            EndPoint = ConfigurationManager.AppSettings.Get("endpoint");
            AuthKey = ConfigurationManager.AppSettings.Get("authkey");
        }        

        private async Task initDatabaseAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());         
            }
        }

        private async Task initCollectionAsync()
        {
            try
            {
                collection = await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        public void Initialize()
        {
            if (client == null)
            {
                client = new DocumentClient(new Uri(EndPoint), AuthKey);
                initDatabaseAsync().Wait();
                initCollectionAsync().Wait();
            }
        }

        public List<QAItem> RunSearchQuery(string input)
        {
            //SELECT qa from c
            //JOIN qa IN c.list
            //JOIN word IN qa.keywords WHERE word = "input"

            List<QAItem> results = null;

            if (client != null)
            {
                string query = string.Format("SELECT qa.id,qa.question,qa.response from c JOIN qa IN c.list JOIN word IN qa.keywords WHERE word=\"{0}\"", input);

                results = client.CreateDocumentQuery<QAItem>(collection.SelfLink, query).ToList<QAItem>();
            }

            return results;
        }
    }
}
