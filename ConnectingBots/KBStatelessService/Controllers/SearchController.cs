using KBStatelessService.Models;
using KBStatelessService.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace KBStatelessService.Controllers
{
    public class SearchController : ApiController
    { 
        // GET api/search/keyword
        public string Get(string keyword)
        {
            if (keyword == null || keyword == string.Empty) return string.Empty;

            List<QAItem> result = FaqService.Instance.RunSearchQuery(keyword);
            if (result != null)
            {
                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            else
                return string.Empty;
        }
    }
}
