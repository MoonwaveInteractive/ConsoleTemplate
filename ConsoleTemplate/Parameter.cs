// (c) 2020 Moonwave Interactive. All rights reserved under MIT License.

using System;

namespace ConsoleTemplate
{
    /// <summary>
    /// Class that represents a command parameter. This class cannot be inherited.
    /// </summary>
    public sealed class Parameter
    {
        /// <summary>
        /// Gets the name of the <see cref="Parameter"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the usage text of the <see cref="Parameter"/>.
        /// </summary>
        public string UsageText { get; }

        /// <summary>
        /// Creates and intializes a new instance of a <see cref="Parameter"/> object.
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="Parameter"/>.
        /// </param>
        /// <param name="usageText">
        /// The usage text of the <see cref="Parameter"/>.
        /// </param>
        public Parameter(string name, string usageText)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Parameter name cannot be null or emmty.");

            if (string.IsNullOrWhiteSpace(usageText))
                throw new ArgumentNullException("Parameter usage text cannot be null or emoty.");

            Name = name;
            UsageText = usageText;
        }
    }
}