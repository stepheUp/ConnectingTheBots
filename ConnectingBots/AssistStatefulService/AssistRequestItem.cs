using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistStatefulService
{
    public struct AssistRequestItem
    {
        public int IdAssistItem { get; set; }
        public List<string> Messages { get; set; }

    }
}
