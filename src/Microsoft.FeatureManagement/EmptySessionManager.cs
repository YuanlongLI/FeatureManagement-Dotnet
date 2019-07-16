﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
using System.Threading.Tasks;

namespace Microsoft.FeatureManagement
{
    /// <summary>
    /// Empty implementation of <see cref="ISessionManager"/>.
    /// </summary>
    class EmptySessionManager : ISessionManager
    {
        public Task Set(string featureName, bool enabled)
        {
            return Task.CompletedTask;
        }

        public Task<bool> TryGet(string featureName, out bool enabled)
        {
            enabled = false;

            return Task.FromResult(enabled);
        }
    }
}
