using System;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation
{
    /// <summary>
    /// Attribute to associate a property with a <see cref="PartialScoreType"/> for scoring calculations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PartialScoreTypeAttribute : Attribute
    {
        /// <summary>
        /// Gets the partial score type associated with the property.
        /// </summary>
        public PartialScoreType ScoreType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartialScoreTypeAttribute"/> class.
        /// </summary>
        /// <param name="scoreType">The partial score type to associate with the property.</param>
        public PartialScoreTypeAttribute(PartialScoreType scoreType)
        {
            ScoreType = scoreType;
        }
    }
}
