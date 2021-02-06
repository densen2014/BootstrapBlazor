// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// IStringLocalizer 实现类
    /// </summary>
    internal class StringLocalizer : IStringLocalizer
    {
        [NotNull]
        private readonly IStringLocalizer? _localizer;

        public StringLocalizer(IStringLocalizerFactory factory, ILoggerFactory loggerFactory, IOptions<JsonLocalizationOptions> options)
        {
            _localizer = options.Value.StringLocalizer;

            if (_localizer == null)
            {
                var op = Options.Create(new LocalizationOptions() { ResourcesPath = options.Value.ResourcesPath });
                _localizer = new ResourceManagerStringLocalizerFactory(op, loggerFactory).Create("BootstrapBlazor.Server", op.Value.ResourcesPath);
            }
        }

        public LocalizedString this[string name] => _localizer[name];

        public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _localizer.GetAllStrings(includeParentCultures);
    }
}
