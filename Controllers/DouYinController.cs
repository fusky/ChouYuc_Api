using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChouYuc_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DouYinController : ControllerBase
    {
        public Object Get(string url)
        {
            url = HttpHelper.HttpGet(url);
            string vid = Regex.Matches(url, "/(.*?)/")[2].Value.Split('/')[1];
            var html_302 = HttpHelper.HttpGet(url);
            var dytk = Regex.Match(Regex.Match(html_302, "dytk(.*?)}").Value, "\\w+(?=\")").Value;
            var result = HttpHelper.HttpGet("https://www.iesdouyin.com/web/api/v2/aweme/iteminfo/?item_ids=" + vid + "&dytk=" + dytk);
            return result;
        }
    }
}