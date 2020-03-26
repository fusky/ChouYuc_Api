using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChouYuc_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KuaiShouController : ControllerBase
    {
        public Object Get(string url)
        {
            var sig = HttpHelper.StringToMD5Hash("client_key=56c3713cshareText=" + url + System.Text.Encoding.ASCII.GetString(new byte[] { 50, 51, 99, 97, 97, 98, 48, 48, 51, 53, 54, 99 }));
            var data = "client_key=56c3713c&shareText=" + url + "&sig=" + sig;
            var result = HttpHelper.HttpGet("http://api.gifshow.com/rest/n/tokenShare/info/byText?" + data);
            return result;
        }
    }
}