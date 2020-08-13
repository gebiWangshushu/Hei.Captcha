using System;
using Microsoft.Extensions.DependencyInjection;

namespace Hei.Captcha
{
    public static class HeiCaptchaExtension
    {
        /// <summary>
        /// 启用HeiCaptcha
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddHeiCaptcha(this IServiceCollection services, Action<HeiCaptchaOptions> configure = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure != null)
            {
                services.Configure(configure);
            }

            services.AddScoped<SecurityCodeHelper>();
            return services;
        }
    }
}
