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
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Moreland.AspNetCore.SecurityHeaders.Middleware
{
    /// <summary>
    /// Middleware responsible for adding custom header at the end of a request
    /// </summary>
    public class CustomHeaderMiddleware
    {
        private readonly Func<IEnumerable<KeyValuePair<string, StringValues>>> _getHeaders;

        /// <summary>
        /// Instantiates a new instance of the <see cref="CustomHeaderMiddleware"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// if <paramref name="getHeaders"/> 
        /// </exception>
        public CustomHeaderMiddleware(Func<IEnumerable<KeyValuePair<string, StringValues>>> getHeaders)
        {
            _getHeaders = getHeaders ?? throw new ArgumentNullException(nameof(getHeaders));
        }

        /// <summary>
        /// adds a custom header provided to respones prior to running next delegate in the pipeline
        /// </summary>
        /// <param name="context">current request context</param>
        /// <param name="next">next handler in the pipeline</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            GuardAgainst.NullArgument(next);

            if (context == null!)
                return;

            context.Response.Headers.AddRangeIfNotNullOrEmpty(_getHeaders());
            await next(context).ConfigureAwait(true);
        }
    }
}
