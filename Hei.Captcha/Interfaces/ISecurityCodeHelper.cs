/*----------------------------------------------------------------
// Copyright © 2019 Chinairap.All rights reserved. 
// CLR版本：	4.0.30319.42000
// 类 名 称：    ISecurityCodeHelper
// 文 件 名：    ISecurityCodeHelper
// 创建者：      DUWENINK
// 创建日期：	2019/8/2 11:50:52
// 版本	日期					修改人	
// v0.1	2019/8/2 11:50:52	DUWENINK
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Hei.Captcha.Interfaces
{
    /// <summary>
    /// 命名空间： Hei.Captcha.Interfaces
    /// 创建者：   DUWENINK
    /// 创建日期： 2019/8/2 11:50:52
    /// 类名：     ISecurityCodeHelper
    /// </summary>
    public interface ISecurityCodeHelper
    {
        /// <summary>
        /// 生成随机中文字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        string GetRandomCnText(int length);


        /// <summary>
        /// 生成随机英文字母/数字组合字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        string GetRandomEnDigitalText(int length);


        /// <summary>
        /// 获取泡泡样式验证码
        /// </summary>
        /// <param name="text">2-3个文字，中文效果较好</param>
        /// <param name="width">验证码宽度，默认宽度100px，可根据传入</param>
        /// <param name="format"></param>
        /// <returns>验证码图片字节数组</returns>
        byte[] GetBubbleCodeByte(string text);


        /// <summary>
        /// 英文字母+数字组合验证码
        /// </summary>
        /// <param name="text"></param>
        /// <returns>验证码图片字节数组</returns>
        byte[] GetEnDigitalCodeByte(string text);


    }
}
