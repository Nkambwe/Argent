using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Vendors;
using Argent.Api.Domain.Enums;
using System.Numerics;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {

    /// <summary>
    /// A debit or credit card linked to a vendor/trader.
    /// Supports transaction limits and freeze controls.
    /// </summary>
    public class Card : BaseEntity {
        public string Holder { get; set; } = string.Empty;

        [EncryptableAttribute("Card Number")]
        public string CardNumber { get; set; } = string.Empty;

        public CardType Type { get; set; }
        public CardTransactionType TransactionType { get; set; }
        public bool Freeze { get; set; }
        public decimal Limit { get; set; }

        public long VendorId { get; set; }
        public Vendor? Vendor { get; set; }
        public ICollection<CardLedgerEntry> CardLedgerEntries { get; set; } = [];
    }
}
