using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Corentin_Maxence.Models
{
    public class Product
    {
            private ProductStore store;

            public int id { get; set; }

            public string name { get; set; }

            public string description { get; set; }

            public int prix { get; set; }
        
    }
}
