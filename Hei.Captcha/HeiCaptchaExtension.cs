using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hei.Captcha
{
    public static class HeiCaptchaExtension
    {
        /// <summary>
        /// 启用HeiCaptcha
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHeiCaptcha(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<SecurityCodeHelper>();
            return services;
        }
    }
}
