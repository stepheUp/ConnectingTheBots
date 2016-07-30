using System.Collections.Generic;
using System.Web.Http;


namespace AssistStatelessService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    
        // GET api/values/5 
        public string Get(int id)
        {

            return "value";
        }

        // GET api/assist/id
        public string Get(string id)
        {
            // return the last message
            //Binding binding = WcfUtility.CreateTcpClientBinding();

            return "";
        }


        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
