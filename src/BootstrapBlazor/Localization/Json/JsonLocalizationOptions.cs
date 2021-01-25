// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.IO;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// LocalizationOptions 配置类
    /// </summary>
    public class JsonLocalizationOptions : LocalizationOptions
    {
        /// <summary>
        /// 获得/设置 本地化资源文件流集合
        /// </summary>
        public IEnumerable<Stream>? JsonLocalizationStreams { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public JsonLocalizationOptions()
        {
            ResourcesPath = "Locales";
        }
    }
}
