using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTest
{
    class Model
    {
        public string error { get; set; }
        public string base_currency_id { get; set; }
        public string base_currency_name { get; set; }
        public string base_price_last_updated { get; set; }
        public string quote_currency_id { get; set; }
        public string quote_currency_name { get; set; }
        public string quote_price_last_updated { get; set; }
        public Double amount { get; set; }
        public Double price { get; set; }
    }
}
