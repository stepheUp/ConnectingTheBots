using KBStatelessService.Models;
using KBStatelessService.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace KBStatelessService.Controllers
{
    public class SearchController : ApiController
    {
        // GET api/search 
        public IEnumerable<string> Get()
        {            
            return new string[] { "value1", "value2" };
        }

        // GET api/search/5 
        public string Get(int id)
        {
            return "value";
        }

        // GET api/search/keyword
        public string Get(string keyword)
        {
            List<QAItem> result = FaqService.RunQuery(keyword);
            string json = JsonConvert.SerializeObject(result);
            return json;
        }

        // POST api/search 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/search/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/search/5 
        public void Delete(int id)
        {
        }
    }
}
