﻿namespace Durak.Auth.Web.Application
{
    /// <summary>
    /// Swagger controller group attribute
    /// </summary>
    ///
    [AttributeUsage(AttributeTargets.Method)]
    public class FeatureGroupNameAttribute : Attribute
    {
        /// <inheritdoc />
        public FeatureGroupNameAttribute(string groupName) => GroupName = groupName;

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName { get; }
    }
}