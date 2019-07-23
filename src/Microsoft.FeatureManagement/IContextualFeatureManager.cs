﻿namespace Microsoft.FeatureManagement
{
    /// <summary>
    /// Used to evaluate whether a feature is enabled or disabled.
    /// </summary>
    public interface IContextualFeatureManager
    {
        /// <summary>
        /// Checks whether a given feature is enabled.
        /// </summary>
        /// <param name="feature">The name of the feature to check.</param>
        /// <param name="context">A context providing information that can be used to evaluate whether a feature should be on or off.</param>
        /// <returns>True if the feature is enabled, otherwise false.</returns>
        bool IsEnabled<TContext>(string feature, TContext context) where TContext : IFeatureFilterContext;
    }
}
