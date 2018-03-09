// Copyright 2015 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Web;
using Serilog.Core;
using Serilog.Events;

namespace SerilogWeb.Classic.Enrichers
{
    /// <summary>
    /// Enrich log events with the Client User Agent.
    /// </summary>
    public class HttpRequestUserAgentEnricher : ILogEventEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string HttpRequestUserAgentPropertyName = "HttpRequestUserAgent";

        #region Implementation of ILogEventEnricher

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");

            if (HttpContext.Current?.Request == null)
                return;
            
            if (string.IsNullOrWhiteSpace(HttpContext.Current.Request.UserAgent))
                return;

            var userAgent = HttpContext.Current.Request.UserAgent;
            var httpRequestUserAgentProperty = new LogEventProperty(HttpRequestUserAgentPropertyName, new ScalarValue(userAgent));
            logEvent.AddPropertyIfAbsent(httpRequestUserAgentProperty);
        }

        #endregion
    }
}
