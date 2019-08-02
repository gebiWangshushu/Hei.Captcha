/*----------------------------------------------------------------
// Copyright © 2019 Chinairap.All rights reserved. 
// CLR版本：	4.0.30319.42000
// 类 名 称：    ValidatecodeDto
// 文 件 名：    ValidatecodeDto
// 创建者：      DUWENINK
// 创建日期：	2019/8/2 12:02:03
// 版本	日期					修改人	
// v0.1	2019/8/2 12:02:03	DUWENINK
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Dtos
{
    /// <summary>
    /// 命名空间： Demo.Dtos
    /// 创建者：   DUWENINK
    /// 创建日期： 2019/8/2 12:02:03
    /// 类名：     ValidatecodeDto
    /// </summary>
    public class ValidatecodeDto
    {
        /// <summary>
        /// 浏览器的cookie 
        /// </summary>
        public string ValidatecodeFromCookie { get;  set; }
        /// <summary>
        /// 用户输入的值
        /// </summary>
        public string TextByUser { get; set; }
    }
}
