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
    public class HuoShanController : ControllerBase
    {
        public Object Get(string url)
        {
            url = HttpHelper.HttpGet(url);
            var item_id = HttpHelper.GetUrlParameterValue(url, "item_id");
            var json = JsonConvert.DeserializeObject<JObject>(HttpHelper.HttpGet("https://share.huoshan.com/api/item/info?item_id=" + item_id));
            var video_id = HttpHelper.GetUrlParameterValue(json["data"]["item_info"]["url"].ToString(), "video_id");
            var result = HttpHelper.HttpGet("http://hotsoon.snssdk.com/hotsoon/item/video/_playback/?video_id=" + video_id);
            return result;
        }
    }
}