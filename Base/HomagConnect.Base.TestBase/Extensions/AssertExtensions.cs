using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomagConnect.Base.TestBase.Extensions
{
    /// <summary>
    /// Provides custom assertion extensions used by integration and sample tests.
    /// </summary>
    public static class AssertExtensions
    {
        /// <summary>
        /// Asserts that a collection is either <see langword="null"/> or contains no elements.
        /// </summary>
        /// <typeparam name="T">The collection element type.</typeparam>
        /// <param name="actual">The collection under test.</param>
        /// <param name="customMessage">Optional custom assertion message.</param>
        public static void ShouldBeNullOrEmpty<T>(
            this IEnumerable<T>? actual,
            string? customMessage = null)
        {
            if (actual == null)
            {
                return;
            }

            if (actual.Any()) 
            {
                Assert.Fail(customMessage ?? "Collection should be empty.");
            }
        }

        /// <summary>
        /// Asserts that a collection is not <see langword="null"/> and contains
        /// at least one element.
        /// </summary>
        /// <typeparam name="T">The collection element type.</typeparam>
        /// <param name="actual">The collection under test.</param>
        /// <param name="customMessage">Optional custom assertion message.</param>
        public static void ShouldNotBeNullOrEmpty<T>(
            this IEnumerable<T>? actual,
            string? customMessage = null)
        {
            Assert.IsNotNull(actual, customMessage ?? "Collection should not be null.");
            Assert.IsTrue(actual.Any(), customMessage ?? "Collection should not be empty.");
        }

        /// <summary>
        /// Asserts that a nullable <see cref="Guid"/> has a value and that value is
        /// not <see cref="Guid.Empty"/>.
        /// </summary>
        /// <param name="actual">The nullable <see cref="Guid"/> under test.</param>
        /// <param name="customMessage">Optional custom assertion message.</param>
        public static void ShouldNotBeNullOrEmpty(
            this Guid? actual,
            string? customMessage = null)
        {
            Assert.IsNotNull(actual, customMessage ?? "Guid should not be null.");
            Assert.AreNotEqual(Guid.Empty, actual.Value, customMessage ?? "Guid should not be empty.");
        }
    }
}
