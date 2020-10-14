﻿//
// Copyright © 2020 Terry Moreland
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Moreland.AspNetCore.SecurityHeaders.Middleware
{
    /// <summary>
    /// XFrameOptions Middleware providing support for adding X-Frame-Options header
    /// </summary>
    public class XFrameOptionsMiddleware
    {
        private readonly IOptions<XFrameOptions> _options;
        private readonly CustomHeaderMiddleware _middleware;

        /// <summary>
        /// Instantiates a new instance of the <see cref="XFrameOptionsMiddleware"/> class.
        /// </summary>
        /// <param name="options">middleware options</param>
        /// <exception cref="ArgumentNullException">
        /// if <paramref name="options"/> is null.
        /// </exception>
        public XFrameOptionsMiddleware(IOptions<XFrameOptions> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _middleware = new CustomHeaderMiddleware(() => "X-Frame-Options", GetHeaderValue);
        }

        /// <summary>
        /// runs the next middleware in the chain then adds X-FrameOptions header value
        /// </summary>
        /// <param name="context">current request context</param>
        /// <param name="next">next handler in the pipeline</param>
        /// <returns></returns>
        public Task InvokeAsync(HttpContext context, RequestDelegate next) => 
            _middleware.InvokeAsync(context, next);

        private string GetHeaderValue()
        {
            var headerValue = _options.Value.Type.GetDescriptionOrEmpty();
            if (string.IsNullOrEmpty(headerValue))
                return string.Empty;

            return (_options.Value.Type == XFrameOptionValue.AllowFrom && _options.Value.AllowFromSource != null)
                ? $"{headerValue} {_options.Value.AllowFromSource}"
                : headerValue;
        }
    }
}
