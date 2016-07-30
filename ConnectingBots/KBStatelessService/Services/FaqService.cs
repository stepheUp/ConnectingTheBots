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
        private static readonly string DatabaseId   = ConfigurationManager.AppSettings.Get("database");
        private static readonly string CollectionId = ConfigurationManager.AppSettings.Get("collection");
        private static readonly string EndPoint     = ConfigurationManager.AppSettings.Get("endpoint");
        private static readonly string AuthKey      = ConfigurationManager.AppSettings.Get("authkey");

        private static DocumentClient client;
        private static DocumentCollection collection;

        public static void Initialize()
        {
            client = new DocumentClient(new Uri(EndPoint), AuthKey);
            InitDatabaseAsync().Wait();
            InitCollectionAsync().Wait();
        }

        private static async Task InitDatabaseAsync()
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

        private static async Task InitCollectionAsync()
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

        public static List<QAItem> RunQuery(string input)
        {
            //SELECT qa from c
            //JOIN qa IN c.list
            //JOIN word IN qa.keywords WHERE word = "input"

            string query = string.Format("SELECT qa.id,qa.question,qa.response from c JOIN qa IN c.list JOIN word IN qa.keywords WHERE word=\"{0}\"", input);

            List<QAItem> results = client.CreateDocumentQuery<QAItem>(collection.SelfLink, query).ToList<QAItem>();            

            return results;
        }
    }
}
