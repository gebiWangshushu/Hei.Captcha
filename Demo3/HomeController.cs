using Microsoft.AspNetCore.Mvc;
using Hei.Captcha;

namespace Demo
{
    public class HomeController : Controller
    {
        private readonly SecurityCodeHelper _securityCode;

        public HomeController(SecurityCodeHelper securityCode)
        {
            this._securityCode = securityCode;
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
            var code = _securityCode.GetRandomCnText(2);
            var imgbyte = _securityCode.GetBubbleCodeByte(code);

            return File(imgbyte, "image/png");
        }

        /// <summary>
        /// 数字字母组合验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult HybridCode()
        {
            var code = _securityCode.GetRandomEnDigitalText(4);
            var imgbyte = _securityCode.GetEnDigitalCodeByte(code);

            return File(imgbyte, "image/png");
        }

        /// <summary>
        /// gif泡泡中文验证码 
        /// </summary>
        /// <returns></returns>
        public IActionResult GifBubbleCode()
        {
            var code = _securityCode.GetRandomCnText(2);
            var imgbyte = _securityCode.GetGifBubbleCodeByte(code);

            return File(imgbyte, "image/gif");
        }

        /// <summary>
        /// gif数字字母组合验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult GifHybridCode()
        {
            var code = _securityCode.GetRandomEnDigitalText(4);
            var imgbyte = _securityCode.GetGifEnDigitalCodeByte(code);

            return File(imgbyte, "image/gif");
        }
    }
}
