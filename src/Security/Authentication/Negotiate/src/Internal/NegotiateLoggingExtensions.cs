// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace Microsoft.Extensions.Logging
{
    internal static class NegotiateLoggingExtensions
    {
        private static readonly Action<ILogger, Exception?> _incompleteNegotiateChallenge;
        private static readonly Action<ILogger, Exception?> _negotiateComplete;
        private static readonly Action<ILogger, Exception?> _enablingCredentialPersistence;
        private static readonly Action<ILogger, string, Exception?> _disablingCredentialPersistence;
        private static readonly Action<ILogger, Exception> _exceptionProcessingAuth;
        private static readonly Action<ILogger, Exception> _credentialError;
        private static readonly Action<ILogger, Exception> _clientError;
        private static readonly Action<ILogger, Exception?> _challengeNegotiate;
        private static readonly Action<ILogger, Exception?> _reauthenticating;
        private static readonly Action<ILogger, Exception?> _deferring;
        private static readonly Action<ILogger, string, Exception?> _negotiateError;
        private static readonly Action<ILogger, string, Exception?> _protocolNotSupported;

        static NegotiateLoggingExtensions()
        {
            _incompleteNegotiateChallenge = LoggerMessage.Define(
                eventId: new EventId(1, "IncompleteNegotiateChallenge"),
                logLevel: LogLevel.Information,
                formatString: "Incomplete Negotiate handshake, sending an additional 401 Negotiate challenge.");
            _negotiateComplete = LoggerMessage.Define(
                eventId: new EventId(2, "NegotiateComplete"),
                logLevel: LogLevel.Debug,
                formatString: "Completed Negotiate authentication.");
            _enablingCredentialPersistence = LoggerMessage.Define(
                eventId: new EventId(3, "EnablingCredentialPersistence"),
                logLevel: LogLevel.Debug,
                formatString: "Enabling credential persistence for a complete Kerberos handshake.");
            _disablingCredentialPersistence = LoggerMessage.Define<string>(
                eventId: new EventId(4, "DisablingCredentialPersistence"),
                logLevel: LogLevel.Debug,
                formatString: "Disabling credential persistence for a complete {protocol} handshake.");
            _exceptionProcessingAuth = LoggerMessage.Define(
                eventId: new EventId(5, "ExceptionProcessingAuth"),
                logLevel: LogLevel.Error,
                formatString: "An exception occurred while processing the authentication request.");
            _challengeNegotiate = LoggerMessage.Define(
                eventId: new EventId(6, "ChallengeNegotiate"),
                logLevel: LogLevel.Debug,
                formatString: "Challenged 401 Negotiate.");
            _reauthenticating = LoggerMessage.Define(
                eventId: new EventId(7, "Reauthenticating"),
                logLevel: LogLevel.Debug,
                formatString: "Negotiate data received for an already authenticated connection, Re-authenticating.");
            _deferring = LoggerMessage.Define(
                eventId: new EventId(8, "Deferring"),
                logLevel: LogLevel.Information,
                formatString: "Deferring to the server's implementation of Windows Authentication.");
            _credentialError = LoggerMessage.Define(
                eventId: new EventId(9, "CredentialError"),
                logLevel: LogLevel.Debug,
                formatString: "There was a problem with the users credentials.");
            _clientError = LoggerMessage.Define(
                eventId: new EventId(10, "ClientError"),
                logLevel: LogLevel.Debug,
                formatString: "The users authentication request was invalid.");
            _negotiateError = LoggerMessage.Define<string>(
                eventId: new EventId(11, "NegotiateError"),
                logLevel: LogLevel.Debug,
                formatString: "Negotiate error code: {error}.");
            _protocolNotSupported = LoggerMessage.Define<string>(
                eventId: new EventId(12, "ProtocolNotSupported"),
                logLevel: LogLevel.Debug,
                formatString: "Negotiate is not supported with {protocol}.");
        }

        public static void IncompleteNegotiateChallenge(this ILogger logger)
            => _incompleteNegotiateChallenge(logger, null);

        public static void NegotiateComplete(this ILogger logger)
            => _negotiateComplete(logger, null);

        public static void EnablingCredentialPersistence(this ILogger logger)
            => _enablingCredentialPersistence(logger, null);

        public static void DisablingCredentialPersistence(this ILogger logger, string protocol)
            => _disablingCredentialPersistence(logger, protocol, null);

        public static void ExceptionProcessingAuth(this ILogger logger, Exception ex)
            => _exceptionProcessingAuth(logger, ex);

        public static void ChallengeNegotiate(this ILogger logger)
            => _challengeNegotiate(logger, null);

        public static void Reauthenticating(this ILogger logger)
            => _reauthenticating(logger, null);

        public static void Deferring(this ILogger logger)
            => _deferring(logger, null);

        public static void CredentialError(this ILogger logger, Exception ex)
            => _credentialError(logger, ex);

        public static void ClientError(this ILogger logger, Exception ex)
            => _clientError(logger, ex);

        public static void NegotiateError(this ILogger logger, string error)
            => _negotiateError(logger, error, null);

        public static void ProtocolNotSupported(this ILogger logger, string protocol)
            => _protocolNotSupported(logger, protocol, null);
    }
}
