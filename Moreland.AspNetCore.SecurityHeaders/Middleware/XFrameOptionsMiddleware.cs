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
    /// XFrameOptions Middleware providing support for adding X-Frame-Options header
    /// </summary>
    public class XFrameOptionsMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Instantiates a new instance of the <see cref="XFrameOptionsMiddleware"/> class.
        /// </summary>
        /// <param name="next"></param>
        /// <exception cref="ArgumentNullException">
        /// if <paramref name="next"/> is null.
        /// </exception>
        public XFrameOptionsMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// runs the next middleware in the chain then adds X-FrameOptions header value
        /// </summary>
        /// <param name="context">current request context</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context).ConfigureAwait(true);

            if (context != null!)
                context.Response.Headers.Add("X-Frame-Options", "");

        }
    }
}
