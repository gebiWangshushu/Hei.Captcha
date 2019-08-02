/*----------------------------------------------------------------
// Copyright © 2019 Chinairap.All rights reserved. 
// CLR版本：	4.0.30319.42000
// 类 名 称：    SecurityCodeDI
// 文 件 名：    SecurityCodeDI
// 创建者：      DUWENINK
// 创建日期：	2019/8/2 11:52:28
// 版本	日期					修改人	
// v0.1	2019/8/2 11:52:28	DUWENINK
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using Hei.Captcha.Interfaces;
using Hei.Captcha.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace Hei.Captcha.DI
{
    /// <summary>
    /// 命名空间： Hei.Captcha.DI
    /// 创建者：   DUWENINK
    /// 创建日期： 2019/8/2 11:52:28
    /// 类名：     SecurityCodeDI
    /// </summary>
    public static class SecurityCodeDI
    {
        /// <summary>
        /// 添加验证码支持
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHeiCaptcha(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            //注入一个单例的SecurityCode
            services.TryAdd(ServiceDescriptor.Singleton<ISecurityCodeHelper, SecurityCodeHelper>());
            return services;
        }
    }
}
