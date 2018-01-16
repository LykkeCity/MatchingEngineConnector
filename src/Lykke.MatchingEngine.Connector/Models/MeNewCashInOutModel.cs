﻿using Common;
using Lykke.MatchingEngine.Connector.Abstractions.Models;
using ProtoBuf;
using System;

namespace Lykke.MatchingEngine.Connector.Models
{
    [ProtoContract]
    public class MeNewCashInOutModel
    {
        [ProtoMember(1, IsRequired = true)]
        public string Id { get; set; }

        [ProtoMember(2, IsRequired = true)]
        public string ClientId { get; set; }

        [ProtoMember(3, IsRequired = true)]
        public long DateTime { get; set; }

        [ProtoMember(4, IsRequired = true)]
        public string AssetId { get; set; }

        [ProtoMember(5, IsRequired = true)]
        public double Amount { get; set; }

        [ProtoMember(6, IsRequired = false)]
        public Fee Fee { get; set; }
        
        public static MeNewCashInOutModel Create(string id, string clientId,
            string assetId, double amount, string feeClientId = null, double feeSize = 0.0, FeeSizeType feeSizeType = FeeSizeType.ABSOLUTE)
        {
            var feeAbsolute = 0.0;
            Fee fee = null;
            var feeType = feeSize > 0 ? FeeType.CLIENT_FEE : FeeType.NO_FEE;

            if (feeSize > 0)
            {
                if (feeSizeType == FeeSizeType.ABSOLUTE)
                    feeAbsolute = feeSize;
                if (feeSizeType == FeeSizeType.PERCENTAGE)
                    feeAbsolute = Math.Round(amount * feeSize, 15);

                fee = new Fee()
                {
                    SourceClientId = null,
                    TargetClientId = feeClientId,
                    Size = feeAbsolute,
                    Type = (int)feeType,
                    SizeType = (int)feeSizeType
                };
            }

            return new MeNewCashInOutModel
            {
                Id = id,
                ClientId = clientId,
                DateTime = (long)System.DateTime.UtcNow.ToUnixTime(),
                AssetId = assetId,
                Amount = amount + feeAbsolute,
                Fee = fee
            };
        }
    }
}
