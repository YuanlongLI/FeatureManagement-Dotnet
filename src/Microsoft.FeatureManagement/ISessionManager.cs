﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
using System.Threading.Tasks;

namespace Microsoft.FeatureManagement
{
    /// <summary>
    /// Used to store feature state across a session. The implementor is free to decide what constitutes a session.
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        /// Set the state of a feature to be used for a session.
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="enabled">The state of the feature.</param>
        Task SetAsync(string featureName, bool enabled);

        /// <summary>
        /// Queries the session manager for the session's feature state, if any, for the given feature.
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <returns>A result indicating whether the session was able to provide a the state of the feature, and the state, if any.</returns>
        Task<ReadSessionResult> TryGetAsync(string featureName);
    }
}
