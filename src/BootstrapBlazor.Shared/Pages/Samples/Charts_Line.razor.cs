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
    /// 
    /// </summary>
    public sealed partial class Charts_Line
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private ToastService? ToastService { get; set; }

        [Inject]
        private IJSRuntime? JSRuntime { get; set; }

        private static Random Randomer { get; set; } = new Random();

        private JSInterop<Charts_Line>? Interope { get; set; }

        private Chart? LineChart { get; set; } 

        /// <summary>
        /// 强刷显示控件控制,Hack一下
        /// </summary>
        private bool Show { get; set; } = true;

        private string ClickItemID { get; set; }

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
                if (Interope == null) Interope = new JSInterop<Charts_Line>(JSRuntime);
                await Interope.InvokeVoidAsync(this, "", "_initChart", nameof(ShowToast));
            }
        }


        private static Task<ChartDataSource> OnInit(int dsCount, int daCount)
        {
            var ds = new ChartDataSource();
            ds.Options.XAxes.Add(new ChartAxes() { LabelString = "天数" });
            ds.Options.YAxes.Add(new ChartAxes() { LabelString = "数值" });

            ds.Options.Colors = new Dictionary<string, string>() {
                                    { "blue:", "rgb(54, 162, 235)" },
                                    { "green:", "rgb(75, 192, 192)" },
                                    { "red:", "rgb(255, 99, 132)" },
                                    { "orange:", "rgb(255, 159, 64)" },
                                    { "yellow:", "rgb(255, 205, 86)" },
                                    { "tomato:", "rgb(255, 99, 71)" },
                                    { "pink:", "rgb(255, 192, 203)" },
                                    { "violet:", "rgb(238, 130, 238)" },
                                };

            ds.Labels = Enumerable.Range(1, daCount).Select(i => i.ToString());

            //单独设置每条数据曲线率
            //ds.Data[0].Tension = 0;
            //ds.Data[1].Tension = 0.2d;

            for (var index = 0; index < dsCount; index++)
            {
                var tension =(double) Randomer.Next(0, 10) / 10d;
                ds.Data.Add(new ChartDataset()
                {
                    Tension = tension,
                    Label = $"曲线率 {tension}",
                    Data = Enumerable.Range(1, daCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
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
        /// 强刷控件,重新初始化控件外观
        /// </summary>
        /// <param name="chart"></param>
        private async void ReloadChart(Chart? chart)
        {
            Show = false;
            await InvokeAsync(StateHasChanged);
            await Task.Delay(1);
            Show = true;
            await InvokeAsync(StateHasChanged);
            chart?.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        [JSInvokable]
        public void ShowToast()
        {
            ToastService?.Show(new ToastOption() { Title = "友情提示", Content = "屏幕宽度过小，如果是手机请横屏观看" });
        }

        private void OnItemClick(string i)
        {
            ClickItemID = $"点击图表项目:{i}";
            ToastService?.Information(ClickItemID);
            StateHasChanged();
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
