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
        /// 获得/设置 贝塞尔曲线的线张力. 默认为 0 直线 , 曲线显示推荐值 0.4
        /// <para></para> 单个数据也可单独,设置后全局设置无效,全局变回默认直线。
        /// </summary>
        public double Tension { get; set; } = 0d; 
    }
}
