﻿using System.Collections.Generic;

namespace Lykke.MatchingEngine.Connector.Abstractions.Models
{
    public class MultiLimitOrderResponse
    {
        public string Id { get; set; }
        public MeStatusCodes Status { get; set; }
        public string StatusReason { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
        public string AssetPairId { get; set; }
        public IReadOnlyList<LimitOrderStatusModel> Statuses { get; set; }
    }
}