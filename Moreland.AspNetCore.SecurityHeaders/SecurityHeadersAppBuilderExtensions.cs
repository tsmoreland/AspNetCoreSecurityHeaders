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
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moreland.AspNetCore.SecurityHeaders.Middleware;

namespace Moreland.AspNetCore.SecurityHeaders
{
    /// <summary>
    /// Extension methods used to add and configure security header support middleware
    /// </summary>
    public static class SecurityHeadersAppBuilderExtensions
    {
        /// <summary>
        /// Adds and configues security headers support    
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsBuilder"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// if <paramref name="services"/> is <c>null</c>
        /// </exception>
        public static IServiceCollection AddSecurityHeaders(this IServiceCollection services, Action<XFrameOptions>? optionsBuilder = null)
        {
            GuardAgainst.NullArgument(services);

            if (optionsBuilder != null)
                services.Configure(optionsBuilder);

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// if <paramref name="app"/> is <c>null</c>
        /// </exception>
        public static IApplicationBuilder UseXFrameOptions(this IApplicationBuilder app)
        {
            GuardAgainst.NullArgument(app);

            app.UseMiddleware<XFrameOptionsMiddleware>();
            return app;
        }
    }
}
