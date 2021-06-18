﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TablesCell
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        [NotNull]
        private List<Foo>? Items { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = Foo.GenerateFoo(Localizer);
        }

        private static void OnCellRenderHandler(TableCellArgs args)
        {
            var foo = args.Row as Foo;
            if (foo != null && args.ColumnName == "Name")
            {
                if (foo.Name == "张三 0002" || foo.Name == "Zhangsan 0002")
                {
                    args.Colspan = 2;
                    args.Class = "cell-demo";
                    args.Value = $"{foo.Name} -- {foo.Address} -- {foo.Count}";
                }
            }
        }

        private static IEnumerable<AttributeItem> GetAttributes() => new[]
        {
            new AttributeItem() {
                Name = "Row",
                Description = "当前单元格行数据 请自行转化为绑定模型",
                Type = "object",
                ValueList = " - ",
                DefaultValue = "<TModel>"
            },
            new AttributeItem() {
                Name = "ColumnName",
                Description = "当前单元格绑定列名称",
                Type = "string",
                ValueList = " - ",
                DefaultValue = " - "
            },
            new AttributeItem() {
                Name = "Colspan",
                Description = "合并单元格数量",
                Type = "int",
                ValueList = " - ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "Class",
                Description = "当前单元格样式",
                Type = "string",
                ValueList = " - ",
                DefaultValue = " - "
            },
            new AttributeItem() {
                Name = "Value",
                Description = "当前单元格显示内容",
                Type = "string",
                ValueList = " - ",
                DefaultValue = " - "
            }
        };
    }
}
