using System;
using Demo.Dtos;
using Microsoft.AspNetCore.Mvc;
using Hei.Captcha;
using Hei.Captcha.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Demo
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        private SecurityCodeHelper _securityCode ;

        private readonly IMemoryCache _cache;
        public HomeController(SecurityCodeHelper securityCode, IMemoryCache cache)
        {
            _securityCode = securityCode;
            _cache = cache;
        }


        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 泡泡中文验证码 
        /// </summary>
        /// <returns></returns>
        public IActionResult BubbleCode()
        {
            var code = _securityCode.GetRandomCnText(2);//生成的中文验证码
            var vGuid = Guid.NewGuid().ToString();//guid
            var imgbyte = _securityCode.GetBubbleCodeByte(code);//生成的中文图片
            //相对于现在的过期时间 缓存相对于现在的时间后5分钟失效
            _cache.Set(vGuid, code, new TimeSpan(0, 1, 0));
            Response.Cookies.Append("validatecode", vGuid);//把生成的唯一Guid添加到cookie发送给前端
            return File(imgbyte, "image/png");
        }

        /// <summary>
        /// 数字字母组合验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult HybridCode()
        {
            var code = _securityCode.GetRandomEnDigitalText(4);
            var vGuid = Guid.NewGuid().ToString();//guid
            var imgbyte = _securityCode.GetEnDigitalCodeByte(code);
            //相对于现在的过期时间 缓存相对于现在的时间后5分钟失效
            _cache.Set(vGuid, code, new TimeSpan(0, 1, 0));
            Response.Cookies.Append("validatecode", vGuid);//把生成的唯一Guid添加到cookie发送给前端
            return File(imgbyte, "image/png");
        }


        #region 验证验证码
        /// <summary>
        /// 验证码验证
        /// </summary>
        /// <returns></returns>
        [HttpPost("VerifyCode")]
        public IActionResult VerifyCode([FromBody]ValidatecodeDto filter)
        {
            _cache.TryGetValue(filter.ValidatecodeFromCookie, out string value);
            return Json(string.Equals(value, filter.TextByUser, StringComparison.OrdinalIgnoreCase));
        }

        #endregion
    }
}
