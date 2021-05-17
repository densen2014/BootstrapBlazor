// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// Charts bubble 测试
    /// </summary>
    public sealed partial class Charts_test_bubble
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private ToastService? ToastService { get; set; }

        [Inject]
        private IJSRuntime? JSRuntime { get; set; }

        private static Random Randomer { get; set; } = new Random();

        private JSInterop<Charts_test_bubble>? Interope { get; set; }

        private Chart? LineChart { get; set; }

        private Chart? BarChart { get; set; }

        private Chart? PieChart { get; set; }

        private Chart? DoughnutChart { get; set; }

        private Chart? BubbleChart { get; set; }

        private bool IsCricle { get; set; }

        /// <summary>
        /// 是否合并Bar显示
        /// </summary>
        private bool IsStacked { get; set; } = true;

        /// <summary>
        /// 强刷显示控件控制,Hack一下
        /// </summary>
        private bool Show { get; set; } = true;

        private IEnumerable<string> Colors { get; set; } = new List<string>() { "Red", "Blue", "Green", "Orange", "Yellow", "Tomato", "Pink", "Violet" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null)
            {
                if (Interope == null) Interope = new JSInterop<Charts_test_bubble>(JSRuntime);
                await Interope.InvokeVoidAsync(this, "", "_initChart", nameof(ShowToast));
            }
        }
         

      
        private static Task<ChartDataSource> OnBubbleInit(int dsCount, int daCount)
        {
            var ds = new ChartDataSource
            {
                Labels = Enumerable.Range(1, daCount).Select(i => i.ToString())
            };

            for (var index = 0; index < dsCount; index++)
            {
                ds.Data.Add(new ChartDataset()
                {
                    Label = $"数据集 {index}",
                    Data = Enumerable.Range(1, daCount).Select(i => new
                    {
                        x = Randomer.Next(10, 40),
                        y = Randomer.Next(10, 40),
                        r = Randomer.Next(1, 20)
                    })
                });
            }
            return Task.FromResult(ds);
        }

        private static void RandomData(Chart? chart)
        {
            chart?.Update();
        }

        private void AddDataSet(Chart? chart, ref int dsCount)
        {
            if (dsCount < Colors.Count())
            {
                dsCount++;
                chart?.Update("addDataset");
            }
        }

        private static void RemoveDataSet(Chart? chart, ref int dsCount)
        {
            if (dsCount > 1)
            {
                dsCount--;
                chart?.Update("removeDataset");
            }
        }

        private void AddData(Chart? chart, ref int daCount)
        {
            var limit = (chart?.ChartType ?? ChartType.Line) switch
            {
                ChartType.Line => 14,
                ChartType.Bar => 14,
                ChartType.Bubble => 14,
                _ => Colors.Count()
            };

            if (daCount < limit)
            {
                daCount++;
                chart?.Update("addData");
            }
        }

        private static void RemoveData(Chart? chart, ref int daCount)
        {
            var limit = (chart?.ChartType ?? ChartType.Line) switch
            {
                ChartType.Line => 7,
                ChartType.Bar => 7,
                ChartType.Bubble => 4,
                _ => 2
            };
            if (daCount > limit)
            {
                daCount--;
                chart?.Update("removeData");
            }
        }
          

        /// <summary>
        /// 
        /// </summary>
        [JSInvokable]
        public void ShowToast()
        {
            ToastService?.Show(new ToastOption() { Title = "友情提示", Content = "屏幕宽度过小，如果是手机请横屏观看" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposing) Interope?.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
         
    }
}
