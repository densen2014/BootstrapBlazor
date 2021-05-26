// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Chart 图表组件配置项实体类
    /// </summary>
    public class ChartOptions
    {
        /// <summary>
        /// 获得/设置 ChartTitle 实例
        /// </summary>
        public ChartTitle Title { get; } = new ChartTitle();

        /// <summary>
        /// 获得 X 坐标轴实例集合
        /// </summary>
        public List<ChartAxes> XAxes { get; } = new List<ChartAxes>();

        /// <summary>
        /// 获得/设置 是否显示 X 坐标轴刻度线 默认为 true
        /// </summary>
        public bool ShowXAxesLine { get; set; } = true;

        /// <summary>
        /// 获得 X 坐标轴实例集合
        /// </summary>

        public List<ChartAxes> YAxes { get; } = new List<ChartAxes>();

        /// <summary>
        /// 获得/设置 是否显示 Y 坐标轴刻度线 默认为 true
        /// </summary>
        public bool ShowYAxesLine { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否 适配移动端 默认为 true
        /// </summary>
        public bool Responsive { get; set; } = true;

        /// <summary>
        /// 获得/设置 贝塞尔曲线的线张力. 默认为 0.4 曲线, 设置0为直线
        /// <para></para> 单个数据也可单独,设置后全局设置无效,全局变回默认直线。
        /// </summary>
        public double Tension { get; set; } = 0.4d;

        /// <summary>
        /// 获得/设置 数据显示颜色
        /// </summary>
        public Dictionary<string, string> Colors { get; set; } = new Dictionary<string, string>() {
            { "red:", "rgb(255, 99, 132)" },
            { "blue:", "rgb(54, 162, 235)" },
            { "green:", "rgb(75, 192, 192)" },
            { "orange:", "rgb(255, 159, 64)" },
            { "yellow:", "rgb(255, 205, 86)" },
            { "tomato:", "rgb(255, 99, 71)" },
            { "pink:", "rgb(255, 192, 203)" },
            { "violet:", "rgb(238, 130, 238)" },
        };

    }
}
