// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace Microsoft.Extensions.Logging
{
    internal static class LoggingExtensions
    {
        private static readonly Action<ILogger, Exception?> _obtainRequestToken;
        private static readonly Action<ILogger, Exception?> _obtainAccessToken;
        private static readonly Action<ILogger, Exception?> _retrieveUserDetails;

        static LoggingExtensions()
        {
            _obtainRequestToken = LoggerMessage.Define(
                eventId: new EventId(1, "ObtainRequestToken"),
                logLevel: LogLevel.Debug,
                formatString: "ObtainRequestToken");
            _obtainAccessToken = LoggerMessage.Define(
                eventId: new EventId(2, "ObtainAccessToken"),
                logLevel: LogLevel.Debug,
                formatString: "ObtainAccessToken");
            _retrieveUserDetails = LoggerMessage.Define(
                eventId: new EventId(3, "RetrieveUserDetails"),
                logLevel: LogLevel.Debug,
                formatString: "RetrieveUserDetails");

        }

        public static void ObtainAccessToken(this ILogger logger)
        {
            _obtainAccessToken(logger, null);
        }

        public static void ObtainRequestToken(this ILogger logger)
        {
            _obtainRequestToken(logger, null);
        }

        public static void RetrieveUserDetails(this ILogger logger)
        {
            _retrieveUserDetails(logger, null);
        }
    }
}
