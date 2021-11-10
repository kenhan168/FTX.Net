﻿using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using FTX.Net.Objects.Staking;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FTX.Net.Clients.Rest.Spot;
using FTX.Net.Interfaces.Clients.Rest;

namespace FTX.Net.Clients.Rest
{
    /// <summary>
    /// Staking endpoints
    /// </summary>
    public class FTXClientStaking : IFTXClientStaking
    {
        private readonly FTXClient _baseClient;

        internal FTXClientStaking(FTXClient baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXStake>>> GetStakesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXStake>>(_baseClient.GetUri("staking/stakes"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXUnstakeRequest>>> GetUnstakeRequestsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXUnstakeRequest>>(_baseClient.GetUri("staking/unstake_requests"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXStakeBalance>>> GetStakeBalancesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXStakeBalance>>(_baseClient.GetUri("staking/balances"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<FTXUnstakeRequest>> RequestUnstakeAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("coin", asset);
            parameters.AddParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXUnstakeRequest>(_baseClient.GetUri("staking/unstake_requests"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<string[]>> CancelUnstakeRequestAsync(long requestId, CancellationToken ct = default)
        {
            // Doesn't seem to work?
            return await _baseClient.SendFTXRequest<string[]>(_baseClient.GetUri("staking/unstake_requests/" + requestId), HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<FTXStakeReward>>> GetStakingRewardsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendFTXRequest<IEnumerable<FTXStakeReward>>(_baseClient.GetUri("staking/staking_rewards"), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<WebCallResult<FTXStake>> StakeAsync(string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("coin", asset);
            parameters.AddOptionalParameter("size", quantity.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendFTXRequest<FTXStake>(_baseClient.GetUri("staking/stakes"), HttpMethod.Post, ct, parameters, signed: true).ConfigureAwait(false);
        }
    }
}