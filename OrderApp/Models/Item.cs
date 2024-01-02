using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Models
{
    public class Item
    {
        public Item(int id, string eng_name, string esp_name)
        {
            this.id = id;
            this.eng_name = eng_name;
            this.esp_name = esp_name;
        }

        public int id { get; set; }
        public string eng_name { get; set; }
        public string esp_name { get; set; }
    }
}
