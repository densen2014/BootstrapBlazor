﻿(function ($) {
    window.chartColors = {
        red: 'rgb(255, 99, 132)',
        blue: 'rgb(54, 162, 235)',
        green: 'rgb(75, 192, 192)',
        orange: 'rgb(255, 159, 64)',
        yellow: 'rgb(255, 205, 86)',
        tomato: 'rgb(255, 99, 71)',
        pink: 'rgb(255, 192, 203)',
        violet: 'rgb(238, 130, 238)'
    };

    window.lineOption = {
        useSegment: true,
        skipped: (ctx, value) => ctx.p0.skip || ctx.p1.skip ? value : undefined,
        down: (ctx, value) => ctx.p0.parsed.y > ctx.p1.parsed.y ? value : undefined,
        genericOptions: {
            fill: false,
            interaction: {
                intersect: false
            },
            radius: 0
        }
    };

    window.chartOption = {
        options: {
            responsive: true,
            title: {
                display: true,
                text: 'Chart'
            },
            tooltips: {
                mode: 'index',
                intersect: false
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: ''
                    }
                }],
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: ''
                    }
                }]
            },
            onClick: function (c, i) {
                //var activeElement = chart.getElementAtEvent(evt)
                console.debug('handleClick', c,i)

                e = i[0];
                console.log(e._index)
                var x_value = this.data.labels[e._index];
                var y_value = this.data.datasets[0].data[e._index];
                console.log(x_value);
                console.log(y_value);
            }
        }
    };

    $.extend({

        handleClick: function (evt) {
            var activeElement = chart.getElementAtEvent(evt)
            console.debug('handleClick',activeElement)
        },
        getChartOption: function (option) {
            var colors = [];
            var chartColors = option.options.colors ?? window.chartColors;
            for (var name in chartColors) colors.push(name);

            var config = {};
            var colorFunc = null;
            if (option.type === 'line') {
                if (lineOption.useSegment && $.isArray(option.data)) {
                    $.each(option.data, function (i, ele) {
                        $.each(ele.data, function (j, el) {
                            if (el === null) {
                                option.data[i].data[j] = NaN;
                                if (option.data[i].segment === undefined) {
                                    option.data[i].segment = {
                                        borderColor: ctx => lineOption.skipped(ctx, 'rgb(0,0,0,0.2)'), // || down(ctx, 'rgb(192,75,75)'),
                                        borderDash: ctx => lineOption.skipped(ctx, [6, 6])
                                    };
                                }
                            }
                        });
                    });
                }

                config = $.extend(true, {}, chartOption);
                colorFunc = function (data) {
                    var color = chartColors[colors.shift()]
                    $.extend(data, {                  
                        backgroundColor: color,
                        borderColor: color
                    });
                }
            }
            else if (option.type === 'bar') {
                //chartOption.scales = $.extend(true, chartOption.scales, {
                //    x: {
                //        stacked: true,
                //    },
                //    y: {
                //        stacked: true
                //    }
                //});
                config = $.extend(true, {}, chartOption);
                console.log(config);
                colorFunc = function (data) {
                    var color = chartColors[colors.shift()]
                    $.extend(data, {
                        backgroundColor: Chart.helpers.color(color).alpha(0.5).rgbString(),
                        borderColor: color,
                        borderWidth: 1
                    });
                }
            }
            else if (option.type === 'pie' || option.type === 'doughnut') {
                config = $.extend(true, {}, chartOption);
                colorFunc = function (data) {
                    $.extend(data, {
                        backgroundColor: colors.slice(0, data.data.length).map(function (name) {
                            return chartColors[name];
                        })
                    });
                }

                if (option.type === 'doughnut') {
                    $.extend(config.options, {
                        cutoutPercentage: 50,
                        animation: {
                            animateScale: true,
                            animateRotate: true
                        }
                    });
                }
            }
            else if (option.type === 'bubble') {
                config = $.extend(true, {}, chartOption, {
                    data: {
                        animation: {
                            duration: 10000
                        },
                    },
                    options: {
                        tooltips: {
                            mode: 'point'
                        }
                    }
                });
                colorFunc = function (data) {
                    var color = chartColors[colors.shift()]
                    $.extend(data, {
                        backgroundColor: Chart.helpers.color(color).alpha(0.5).rgbString(),
                        borderWidth: 1,
                        borderColor: color
                    });
                }
                console.log("bubble ", config)
            }

            $.each(option.data, function () {
                colorFunc(this);
            });

            console.log("tension", option.data[0]);

            return $.extend(true, config, {
                type: option.type,
                data: {
                    labels: option.labels,
                    datasets: option.data
                },
                options: {
                    responsive: option.options.responsive,
                    title: option.options.title,
                    tension: option.options.tension,
                    fill: true,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: option.options.title.display,
                            text: option.options.title.text
                        }
                    },
                    scales: {
                        xAxes: {
                            stacked: option.options?.xAxes[0]?.stacked,
                            display: option.options.showXAxesLine,
                            scaleLabel: option.options.xAxes
                        },
                        yAxes: {
                            stacked: option.options?.yAxes[0]?.stacked,
                            display: option.options.showXAxesLine,
                            scaleLabel: option.options.yAxes
                        }
                    }//,
                    //scales: {
                    //    xAxes: option.options.xAxes.map(function (v) {
                    //        return {
                    //            display: option.options.showXAxesLine,
                    //            scaleLabel: v
                    //        };
                    //    }),
                    //    yAxes: option.options.yAxes.map(function (v) {
                    //        return {
                    //            display: option.options.showYAxesLine,
                    //            scaleLabel: v
                    //        }
                    //    })
                    //}
                }
            });
        },
        updateChart: function (config, option) {
            if (option.updateMethod === "addDataset") {
                config.data.datasets.push(option.data.datasets.pop());
            }
            else if (option.updateMethod === "removeDataset") {
                config.data.datasets.pop();
            }
            else if (option.updateMethod === "addData") {
                if (config.data.datasets.length > 0) {
                    config.data.labels.push(option.data.labels.pop());
                    config.data.datasets.forEach(function (dataset, index) {
                        dataset.data.push(option.data.datasets[index].data.pop());
                        if (option.type === 'pie' || option.type === 'doughnut') {
                            dataset.backgroundColor.push(option.data.datasets[index].backgroundColor.pop());
                        }
                    });
                }
            }
            else if (option.updateMethod === "removeData") {
                config.data.labels.pop(); // remove the label first

                config.data.datasets.forEach(function (dataset) {
                    dataset.data.pop();
                    if (option.type === 'pie' || option.type === 'doughnut') {
                        dataset.backgroundColor.pop();
                    }
                });
            }
            else if (option.updateMethod === "setAngle") {
                if (option.type === 'doughnut') {
                    if (option.angle === 360) {
                        config.options.circumference = Math.PI;
                        config.options.rotation = -Math.PI;
                    }
                    else {
                        config.options.circumference = 2 * Math.PI;
                        config.options.rotation = -Math.PI / 2;
                    }
                }
            }
            else {
                config.data.datasets.forEach(function (dataset, index) {
                    dataset.data = option.data.datasets[index].data;
                });
            }
        },
        chart: function (el, obj, method, option, updateMethod, type, angle) {
            if ($.isFunction(Chart)) {
                var $el = $(el);
                option.type = type;
                var chart = $el.data('chart');
                if (!chart) {
                    var op = $.getChartOption(option);
                    console.log("chart ", op)
                    $el.data('chart', chart = new Chart(el.getElementsByTagName('canvas'), op));
                    $el.removeClass('is-loading').trigger('chart.afterInit');
                    obj.invokeMethodAsync(method);
                }
                else {
                    var op = $.getChartOption(option);
                    op.angle = angle;
                    op.updateMethod = updateMethod;
                    $.updateChart(chart.config, op);
                    chart.update();
                }
            }
        }
    });
})(jQuery);
