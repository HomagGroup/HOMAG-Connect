﻿using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.Base.TestBase
{
    /// <summary>
    /// Provides a base class for HOMAG Connect tests.
    /// </summary>
    public class TestBase
    {
        #region Private Properties

        private IConfigurationRoot? Configuration { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public virtual TestContext? TestContext { get; set; }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the user secrets ID.
        /// </summary>
        protected virtual string UserSecretsId
        {
            get
            {
                var customAttribute = GetType().Assembly.GetCustomAttribute<UserSecretsIdAttribute>();

                if (customAttribute == null)
                {
                    throw new InvalidOperationException("UserSecretsIdAttribute is missing on the assembly.");
                }

                return customAttribute.UserSecretsId;
            }
        }

        /// <summary>
        /// Gets the authorization key.
        /// </summary>
        protected string AuthorizationKey
        {
            get
            {
                var authorizationKey = GetConfigurationSetting("HomagConnect:AuthorizationKey");

                if (!string.IsNullOrWhiteSpace(authorizationKey))
                {
                    return authorizationKey;
                }

                authorizationKey = GetConfigurationSetting("AuthorizationKey");

                if (!string.IsNullOrWhiteSpace(authorizationKey))
                {
                    return authorizationKey;
                }

                throw new ConfigurationErrorsException($"{nameof(AuthorizationKey)} in appSettings, user secrets, environment variable or run settings must not be null or whitespace.");
            }
        }

        /// <summary>
        /// Gets the base uri.
        /// </summary>
        protected Uri BaseUrl
        {
            get
            {
                var baseUrl = GetConfigurationSetting("HomagConnect:BaseUrl");

                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    return new Uri(baseUrl);
                }

                baseUrl = GetConfigurationSetting("BaseUrl");

                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    return new Uri(baseUrl);
                }

                throw new ConfigurationErrorsException($"{nameof(BaseUrl)} in appSettings, user secrets, environment variable or run settings must not be null or whitespace.");
            }
        }

        /// <summary>
        /// Gets the subscription id.
        /// </summary>
        protected Guid SubscriptionId
        {
            get
            {
                var subscriptionId = GetConfigurationSetting("HomagConnect:SubscriptionId");

                if (string.IsNullOrWhiteSpace(subscriptionId))
                {
                    subscriptionId = GetConfigurationSetting("SubscriptionId");
                }

                if (string.IsNullOrWhiteSpace(subscriptionId))
                {
                    throw new ConfigurationErrorsException($"{nameof(SubscriptionId)} in appSettings, user secrets, environment variable or run settings must not be null or whitespace.");
                }

                Assert.IsTrue(Guid.TryParse(subscriptionId, CultureInfo.InvariantCulture, out var guid), "SubscriptionId in appSettings json must be the subscription id which must be a GUID.");

                return guid;
            }
        }

        #endregion

        /// <summary>
        /// Encodes a username and authorization key to a base64 token.
        /// </summary>
        protected static string EncodeBase64Token(string username, string authorizationKey)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{authorizationKey}"));
        }

        /// <summary>
        /// Gets a configuration setting.
        /// </summary>
        protected virtual string? GetConfigurationSetting(string key)
        {
            var config = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);

            if (config != null)
            {
                return config;
            }

            if (TestContext != null && TestContext.Properties.Contains(key))
            {
                var value = TestContext.Properties[key];

                if (value != null)
                {
                    return value.ToString() ?? string.Empty;
                }
            }

            Configuration ??= new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets(UserSecretsId)
                .Build();

            config = Configuration[key];

            return config ?? null;
        }
    }
}