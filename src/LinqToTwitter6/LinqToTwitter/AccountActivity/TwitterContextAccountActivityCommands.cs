﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LinqToTwitter
{
    public partial class TwitterContext
    {
        /// <summary>
        /// Adds a user subscription to specified webhook
        /// </summary>
        /// <param name="webhookID">ID of webhook user is subscribing to.</param>
        /// <returns>Account Activity data.</returns>
        /// <exception cref="TwitterQueryException">
        /// Throws TwitterQueryException when an AddAccountActivitySubscriptionAsync fails.
        /// </exception>
        public async Task<AccountActivity?> AddAccountActivitySubscriptionAsync(ulong webhookID, CancellationToken cancelToken = default)
        {
            if (webhookID == default)
                throw new ArgumentException($"{nameof(webhookID)} must be set.", nameof(webhookID));

            var newUrl = BaseUrl + $"account_activity/webhooks/{webhookID}/subscriptions.json";

            var accActValue = new AccountActivityValue();

            RawResult =
                await TwitterExecutor.SendJsonToTwitterAsync(
                    HttpMethod.Post.ToString(),
                    newUrl,
                    new Dictionary<string, string>(),
                    accActValue,
                    cancelToken)
                   .ConfigureAwait(false);

            var reqProc = new AccountActivityRequestProcessor<AccountActivity>();
            AccountActivity? accAct = reqProc.ProcessActionResult(RawResult, AccountActivityType.Subscriptions);

            if (accAct != null)
                accAct.WebhookID = webhookID;

            return accAct;
        }

        /// <summary>
        /// Adds a new webhook to account
        /// </summary>
        /// <param name="url">Url of webhook.</param>
        /// <returns>Account Activity data.</returns>
        public async Task<AccountActivity?> AddAccountActivityWebhookAsync(string url, CancellationToken cancelToken = default)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException($"{nameof(url)} must be set.", nameof(url));

            var newUrl = BaseUrl + $"account_activity/webhooks.json";

            RawResult =
                await TwitterExecutor.PostFormUrlEncodedToTwitterAsync<AccountActivity>(
                    HttpMethod.Post.ToString(),
                    newUrl,
                    new Dictionary<string, string?>
                    {
                        { "url", url }
                    },
                    cancelToken)
                    .ConfigureAwait(false);

            var reqProc = new AccountActivityRequestProcessor<AccountActivity>();
            AccountActivity? accAct = reqProc.ProcessActionResult(RawResult, AccountActivityType.Webhooks);

            if (accAct != null)
                accAct.Url = url;

            return accAct;
        }

        /// <summary>
        /// Sends a CRC check to a webhook for testing
        /// </summary>
        /// <param name="webhookID">ID of webhook to send CRC to.</param>
        /// <returns>Account Activity data.</returns>
        public async Task<AccountActivity?> SendAccountActivityCrcAsync(ulong webhookID, CancellationToken cancelToken = default)
        {
            if (webhookID == default)
                throw new ArgumentException($"{nameof(webhookID)} must be set.", nameof(webhookID));

            var newUrl = BaseUrl + $"account_activity/webhooks/{webhookID}.json";

            var accActValue = new AccountActivityValue();

            RawResult =
                await TwitterExecutor.SendJsonToTwitterAsync(
                    HttpMethod.Put.ToString(),
                    newUrl,
                    new Dictionary<string, string>(),
                    accActValue,
                    cancelToken)
                   .ConfigureAwait(false);

            var reqProc = new AccountActivityRequestProcessor<AccountActivity>();
            AccountActivity? accAct = reqProc.ProcessActionResult(RawResult, AccountActivityType.Webhooks);

            if (accAct != null)
                accAct.WebhookID = webhookID;

            return accAct;
        }

        /// <summary>
        /// Deletes a user subscription to specified webhook
        /// </summary>
        /// <param name="webhookID">ID of webhook user is subscribing to.</param>
        /// <returns>Account Activity data.</returns>
        public async Task<AccountActivity?> DeleteAccountActivitySubscriptionAsync(ulong webhookID, CancellationToken cancelToken = default)
        {
            if (webhookID == default)
                throw new ArgumentException($"{nameof(webhookID)} must be set.", nameof(webhookID));

            var newUrl = BaseUrl + $"account_activity/webhooks/{webhookID}/subscriptions.json";

            var accActValue = new AccountActivityValue();

            RawResult =
                await TwitterExecutor.SendJsonToTwitterAsync(
                    HttpMethod.Delete.ToString(),
                    newUrl,
                    new Dictionary<string, string>(),
                    accActValue,
                    cancelToken)
                   .ConfigureAwait(false);

            var reqProc = new AccountActivityRequestProcessor<AccountActivity>();
            AccountActivity? accAct = reqProc.ProcessActionResult(RawResult, AccountActivityType.Subscriptions);

            if (accAct != null)
                accAct.WebhookID = webhookID;

            return accAct;
        }

        /// <summary>
        /// Deletes a new webhook to account
        /// </summary>
        /// <param name="webhookID">Url of webhook.</param>
        /// <returns>Account Activity data.</returns>
        public async Task<AccountActivity?> DeleteAccountActivityWebhookAsync(ulong webhookID, CancellationToken cancelToken = default)
        {
            if (webhookID == default)
                throw new ArgumentException($"{nameof(webhookID)} must be set.", nameof(webhookID));

            var newUrl = BaseUrl + $"account_activity/webhooks/{webhookID}.json";

            var accActValue = new AccountActivity
            {
                WebhookID = webhookID
            };

            RawResult =
                await TwitterExecutor.SendJsonToTwitterAsync(
                    HttpMethod.Delete.ToString(),
                    newUrl,
                    new Dictionary<string, string>(),
                    accActValue,
                    cancelToken)
                   .ConfigureAwait(false);

            var reqProc = new AccountActivityRequestProcessor<AccountActivity>();
            AccountActivity? accAct = reqProc.ProcessActionResult(RawResult, AccountActivityType.Webhooks);

            if (accAct != null)
                accAct.WebhookID = webhookID;

            return accAct;
        }
    }
}
