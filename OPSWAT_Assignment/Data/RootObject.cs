using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPSWAT_Assignment.Data
{
    public class RootObject
    {
        public string data_id { get; set; }
        public string status { get; set; }
        public int in_queue { get; set; }
        public string queue_priority { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }
    }
}
