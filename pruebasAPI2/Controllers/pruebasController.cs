using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace pruebasAPI2.Controllers
{
    [Route("api/[controller]")]
    public class pruebasController : Controller{

        [HttpGet]
        [Route("primero")]
        public bool sinParametros(){
            return true;
        }

        [HttpPost]
        [Route("segundo")]
        public bool sinparametroPost(){
            return true;
        }

        [HttpPost]
        [Route("tercero")]
        public string conparametroPost(string param1){
            return "salio: " + param1;
        }

        [HttpPost]
        [Route("cuarto")]
        public string conparametrosPost(string param1, int param2){
            return "salio: " + param1 + param2.ToString();
        }

    }
}