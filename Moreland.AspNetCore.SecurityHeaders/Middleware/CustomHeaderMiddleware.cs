//
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

namespace Moreland.AspNetCore.SecurityHeaders.Middleware
{
    /// <summary>
    /// Middleware responsible for adding custom header at the end of a request
    /// </summary>
    public class CustomHeaderMiddleware
    {
        private readonly Func<string> _getHeaderName;
        private readonly Func<string> _getHeaderValue;

        /// <summary>
        /// Instantiates a new instance of the <see cref="CustomHeaderMiddleware"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// if <paramref name="getHeaderName"/> or <paramref name="getHeaderValue"/> is <c>null</c>
        /// </exception>
        public CustomHeaderMiddleware(Func<string> getHeaderName, Func<string> getHeaderValue)
        {
            _getHeaderName = getHeaderName ?? throw new ArgumentNullException(nameof(getHeaderName));
            _getHeaderValue = getHeaderValue ?? throw new ArgumentNullException(nameof(getHeaderValue));
        }

        /// <summary>
        /// runs the next middleware in the chain then adds X-FrameOptions header value
        /// </summary>
        /// <param name="context">current request context</param>
        /// <param name="next">next handler in the pipeline</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            GuardAgainst.NullArgument(next);

            await next(context).ConfigureAwait(true);

            if (context == null!)
                return;

            var name = _getHeaderName();
            var value = _getHeaderValue();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                return;

            context.Response.Headers.Add(name, value);
        }
    }
}
