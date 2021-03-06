// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;

namespace Microsoft.AspNetCore.Components
{
    internal class ProtectedPrerenderComponentApplicationStore : PrerenderComponentApplicationStore
    {
        private IDataProtector _protector;

        public ProtectedPrerenderComponentApplicationStore(IDataProtectionProvider dataProtectionProvider) : base()
        {
            CreateProtector(dataProtectionProvider);
        }

        public ProtectedPrerenderComponentApplicationStore(string existingState, IDataProtectionProvider dataProtectionProvider)
        {
            CreateProtector(dataProtectionProvider);
            ExistingState = JsonSerializer.Deserialize<Dictionary<string, byte[]>>(_protector.Unprotect(Convert.FromBase64String(existingState)));
        }

        protected override byte[] SerializeState(IReadOnlyDictionary<string, byte[]> state)
        {
            var bytes = base.SerializeState(state);
            if (_protector != null)
            {
                bytes = _protector.Protect(bytes);
            }

            return bytes;
        }

        private void CreateProtector(IDataProtectionProvider dataProtectionProvider) =>
            _protector = dataProtectionProvider.CreateProtector("Microsoft.AspNetCore.Components.Server.State");
    }
}
