﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ViewzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MDController : ControllerBase
    {
        [HttpPost] 
        public ContentResult Post([FromForm]string MD)
        { 
            string output;

            if (MD.Contains("#"))
            {
                string updated = MD.Replace("#","");
                output = $"<h1>{updated}</h1>";
            }
            else
            {
                output = MD;
            }

            return base.Content(output, "text/html");
            
        }
         
    }
}