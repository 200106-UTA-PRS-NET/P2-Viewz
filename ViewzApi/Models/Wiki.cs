using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewzApi.Models
{
    public class Wiki
    {
        private readonly ILogger _logger;
        public Wiki(ILogger<Wiki> logger)
        {
            _logger = logger;
        }

        public string Url { get; set; }
        public string PageName { get; set; }
        public string Description { get; set; }
    }
}
