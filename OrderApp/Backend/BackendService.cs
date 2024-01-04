using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Backend
{
    public class BackendService : IBackendService
    {
        private readonly IConfiguration _configuration;
        private string ip;
        private string port;
        public bool IsConnected { get; set; }

        public BackendService(IConfiguration configuration) 
        {
            _configuration = configuration;
            var settings = _configuration.GetRequiredSection("Settings").Get<Settings>();
            ip = settings.Ip;
            port = settings.Port;
        }

        public string Connect() 
        {
            var settings = _configuration.GetRequiredSection("Settings").Get<Settings>();
            ip = settings.Ip;
            port = settings.Port;
            return ip + " " + port;
        }
    }
}