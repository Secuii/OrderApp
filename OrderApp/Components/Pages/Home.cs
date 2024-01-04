using Newtonsoft.Json;
using OrderApp.Backend;
using OrderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;

namespace OrderApp.Components.Pages
{
    partial class Home
    {
        public ItemsCollection items;
        protected override async Task OnInitializedAsync()
        {
            await GetItem();
        }

        public async Task GetItem()
        {
            string url = "https://localhost:7003/Order";

            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";

            using WebResponse webResponse = request.GetResponse();
            using Stream webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            string data = reader.ReadToEnd();

#pragma warning disable CS8601 // Possible null reference assignment.
            items = JsonConvert.DeserializeObject<ItemsCollection>(data);
#pragma warning restore CS8601 // Possible null reference assignment.

            Console.WriteLine(data);
        }
    }
}
